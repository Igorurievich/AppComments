using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreChat.Services;
using App.Comments.Data;
using App.Comments.Data.Repositories;
using App.Comments.Common.Interfaces.Repositories;
using App.Comments.Common.Interfaces.Services;
using App.Comments.Common.Services;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using App.Comments.Web;

namespace NetCoreChat
{
	public class Startup
	{
		private IHostingEnvironment CurrentEnvironment { get; set; }
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration, IHostingEnvironment env)
		{
			Configuration = configuration;
			CurrentEnvironment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile("appsettings-Shared.json", optional: false)
				.AddJsonFile("secrets.json", optional: false)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options => options.AddPolicy("AllowAny", x => {
				x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
			}));

			services.AddDbContext<CommentsContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddTransient(typeof(ICommentRepository), typeof(CommentRepository));
			services.AddTransient(typeof(IApplicationUserRepository), typeof(UserRepository));

			services.AddTransient(typeof(ICommentsService), typeof(CommentsService));
			services.AddTransient(typeof(IAuthenticationService), typeof(AuthenticationService));

			services.AddAuthentication().AddFacebook(facebookOptions =>
			{
				facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
				facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
			});

			services.AddTransient<IEmailSender, EmailSender>();
			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			});

			services.AddSignalR();

			services.AddAutoMapper();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseCors("AllowAny");
			
			app.UseCors(builder =>
				builder.WithOrigins("http://example.com")
					.AllowAnyHeader()
				);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			//app.UseAuthentication();

			app.UseWebSockets();

			app.UseMvcWithDefaultRoute();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "DefaultApi",
					template: "api/{controller}/{action}");
			});

			app.UseSignalR(routes =>  // <-- SignalR
			{
				routes.MapHub<CommentsPublisher>("commentsPublisher");
			});

			RouteBuilder routeBuilder = new RouteBuilder(app);
		}
	}
}
