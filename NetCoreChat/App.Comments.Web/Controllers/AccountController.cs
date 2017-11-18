using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Comments.Common.Entities;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using App.Comments.Common.Interfaces.Services;
using App.Comments.Common.Helpers;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace NetCoreChat.Controllers
{
	[Route("api/[controller]/[action]")]
	public class AccountController : Controller
	{
		private readonly IAuthenticationService _authenticationService;

		public AccountController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[AllowAnonymous]
		[HttpGet]
		public string LogInUserWithFacebook(string username, string email)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
			var user = _authenticationService.GetUserByUserNameAndEmail(username, email);
			if (user != null)
			{
				return GenerateJWTBasedOnUser(user);
			}
			return string.Empty;
		}

		[AllowAnonymous]
		[HttpGet]
		public ActionResult LogInUser(string username, string password)
		{
			string jwt = String.Empty;
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
			var user = _authenticationService.LogIn(username, password);
			if (user != null)
			{
				return StatusCode(200, GenerateJWTBasedOnUser(user));
			}
			else if (user == null)
			{
				return StatusCode(400, ResponseMessagesHelper.LogInPassword);
			}
			return StatusCode(400, ResponseMessagesHelper.LogInPassword);
		}

		[HttpPost]
		[AllowAnonymous]
		public void Register([FromForm]string username, [FromForm]string password, [FromForm]string email)
		{
			Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();

			if (ModelState.IsValid && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email))
			{
				var checkUser = _authenticationService.GetUserByUserNameAndEmail(username, email);
				var chekedUserName = _authenticationService.GetUserByUserName(username);
				if (chekedUserName != null)
				{
					Response.WriteAsync(ResponseMessagesHelper.SignUpUserExist, Encoding.UTF8);
				}

				if (checkUser == null && chekedUserName == null)
				{
					var user = new ApplicationUser { UserName = username, Email = email, Password = password };
					var result = _authenticationService.Register(user);
					if (result)
					{
						Response.WriteAsync(ResponseMessagesHelper.SignUpUserRegistered, Encoding.UTF8);
					}
				}
			}
		}

		[HttpPost]
		[AllowAnonymous]
		public bool RegisterWithFacebook([FromForm]string username, [FromForm]string email)
		{
			if (ModelState.IsValid && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email))
			{
				var checkUser = _authenticationService.GetUserByUserNameAndEmail(username, email);
				if (checkUser == null)
				{
					var user = new ApplicationUser { UserName = username, Email = email };
					var result = _authenticationService.Register(user);
					if (result)
					{
						return true;
					}
				}
			}
			return false;
		}

		[HttpGet]
		[AllowAnonymous]
		public bool IsUserExist(string username, string email)
		{
			if (ModelState.IsValid && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email))
			{
				var result = _authenticationService.GetUserByUserNameAndEmail(username, email);
				if (result != null)
				{
					return true;
				}
			}
			return false;
		}

		[HttpGet]
		[AllowAnonymous]
		public bool CheckUserName(string username)
		{
			if (ModelState.IsValid && !string.IsNullOrEmpty(username))
			{
				var result = _authenticationService.GetUserByUserName(username);
				if (result != null)
				{
					return true;
				}
			}
			return false;
		}

		private string GenerateJWTBasedOnUser(ApplicationUser user)
		{
			string jwt = String.Empty;

			var tokenHandler = new JwtSecurityTokenHandler();
			List<Claim> userClaims = new List<Claim>();
			//add any claims to the userClaims collection that you want to be part of the JWT

			ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.UserName, "TokenAuth"), userClaims);
			DateTime expires = DateTime.Now.AddMinutes(30);

			var key = Encoding.UTF8.GetBytes("application_comments_web_magister_work_mega_secret_ultra_key");
			var signingKey = new SymmetricSecurityKey(key);
			var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

			var securityToken = tokenHandler.CreateJwtSecurityToken(
				subject: identity,
				notBefore: DateTime.Now,
				expires: expires,
				signingCredentials: signingCredentials
				);

			jwt = tokenHandler.WriteToken(securityToken);
			Response.StatusCode = (int)HttpStatusCode.Created;

			return jwt;
		}
	}
}
