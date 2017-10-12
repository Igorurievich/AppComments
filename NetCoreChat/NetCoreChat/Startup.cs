using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreChat.Services;
using App.Comments.Common.Entities;
using App.Comments.Data;
using App.Comments.Data.Repositories;
using App.Comments.Common.Interfaces.Repositories;
using System.IO;

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
      services.AddDbContext<CommentsContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<CommentsContext>()
          .AddDefaultTokenProviders();

      services.AddTransient(typeof(ICommentRepository), typeof(CommentRepository));

      services.AddAuthentication().AddFacebook(facebookOptions =>
      {
        facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
        facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
      });

      services.AddTransient<IEmailSender, EmailSender>();
      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
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

      app.UseAuthentication();

      app.UseWebSockets();

      //app.UseMiddleware<ChatWebSocketMiddleware>();

      //app.UseMvc(routes =>
      //{
      //    routes.MapRoute(
      //        name: "default",
      //        template: "{controller=Home}/{action=Index}/{id?}");
      //});

      app.Use(async (context, next) => {
        await next();
        if (context.Response.StatusCode == 404 &&
           !Path.HasExtension(context.Request.Path.Value) &&
           !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });
      app.UseMvcWithDefaultRoute();
      app.UseDefaultFiles();
      app.UseStaticFiles();
    }
  }
}
