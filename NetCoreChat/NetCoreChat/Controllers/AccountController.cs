using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using App.Comments.Common.Entities;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using App.Comments.Common.Interfaces.Services;

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
		public string LogInUser(string username, string password)
		{
			string jwt = String.Empty;

			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
			var user = _authenticationService.LogIn(username, password);
			if (user != null)
			{
				return GenerateJWTBasedOnUser(user);
			}
			return string.Empty;
		}
		
		[HttpPost]
		[AllowAnonymous]
		public bool Register([FromForm]string username, [FromForm]string password, [FromForm]string email)
		{
			if (ModelState.IsValid && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email))
			{
				var user = new ApplicationUser { UserName = username, Email = email, Password = password };
				var result = _authenticationService.Register(user);
				if (result)
				{
					return true;
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

		private string GenerateJWTBasedOnUser(ApplicationUser user)
		{
			string jwt = String.Empty;

			var tokenHandler = new JwtSecurityTokenHandler();
			List<Claim> userClaims = new List<Claim>();
			//add any claims to the userClaims collection that you want to be part of the JWT

			ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.UserName, "TokenAuth"), userClaims);
			DateTime expires = DateTime.Now.AddMinutes(30); //or whatever

			var key = Encoding.UTF8.GetBytes("application_comments_web_magister_work_key");
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

		#region Crap

		//[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		//{
		//	ViewData["ReturnUrl"] = returnUrl;
		//	if (ModelState.IsValid)
		//	{
		//		// This doesn't count login failures towards account lockout
		//		// To enable password failures to trigger account lockout, set lockoutOnFailure: true
		//		var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
		//		if (result.Succeeded)
		//		{
		//			_logger.LogInformation("User logged in.");
		//			return RedirectToLocal(returnUrl);
		//		}
		//		if (result.RequiresTwoFactor)
		//		{
		//			return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
		//		}
		//		if (result.IsLockedOut)
		//		{
		//			_logger.LogWarning("User account locked out.");
		//			return RedirectToAction(nameof(Lockout));
		//		}
		//		else
		//		{
		//			ModelState.AddModelError(string.Empty, "Invalid login attempt.");
		//			return View(model);
		//		}
		//	}

		//	// If we got this far, something failed, redisplay form
		//	return View(model);
		//}

		//[HttpGet]
		//[AllowAnonymous]
		//public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
		//{
		//	// Ensure the user has gone through the username & password screen first
		//	var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

		//	if (user == null)
		//	{
		//		throw new ApplicationException($"Unable to load two-factor authentication user.");
		//	}

		//	var model = new LoginWith2faViewModel { RememberMe = rememberMe };
		//	ViewData["ReturnUrl"] = returnUrl;

		//	return View(model);
		//}

		//[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return View(model);
		//	}

		//	var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
		//	if (user == null)
		//	{
		//		throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
		//	}

		//	var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

		//	var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

		//	if (result.Succeeded)
		//	{
		//		_logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
		//		return RedirectToLocal(returnUrl);
		//	}
		//	else if (result.IsLockedOut)
		//	{
		//		_logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
		//		return RedirectToAction(nameof(Lockout));
		//	}
		//	else
		//	{
		//		_logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
		//		ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
		//		return View();
		//	}
		//}

		//[HttpGet]
		//[AllowAnonymous]
		//public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
		//{
		//	// Ensure the user has gone through the username & password screen first
		//	var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
		//	if (user == null)
		//	{
		//		throw new ApplicationException($"Unable to load two-factor authentication user.");
		//	}

		//	ViewData["ReturnUrl"] = returnUrl;

		//	return View();
		//}

		//[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return View(model);
		//	}

		//	var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
		//	if (user == null)
		//	{
		//		throw new ApplicationException($"Unable to load two-factor authentication user.");
		//	}

		//	var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

		//	var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

		//	if (result.Succeeded)
		//	{
		//		_logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
		//		return RedirectToLocal(returnUrl);
		//	}
		//	if (result.IsLockedOut)
		//	{
		//		_logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
		//		return RedirectToAction(nameof(Lockout));
		//	}
		//	else
		//	{
		//		_logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
		//		ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
		//		return View();
		//	}
		//}

		//[HttpGet]
		//[AllowAnonymous]
		//public IActionResult Lockout()
		//{
		//	return View();
		//}


		//	// disable pass validation;


		//[HttpPost]
		//[AllowAnonymous]
		//public async Task<bool> Register(string username, string password, string email)
		//{
		//	if (ModelState.IsValid)
		//	{

		//		var user = new ApplicationUser { UserName = username, Email = email };
		//		var result = await _userManager.CreateAsync(user);
		//		if (result.Succeeded)
		//		{
		//			_logger.LogInformation("User created a new account with password.");

		//			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		//			//var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
		//			//await _emailSender.SendEmailConfirmationAsync(email, callbackUrl);

		//			await _signInManager.SignInAsync(user, isPersistent: false);
		//			_logger.LogInformation("User created a new account with password.");
		//			return true;
		//		}
		//		else
		//		{
		//			 var exceptionText = result.Errors.Aggregate("User Creation Failed - Identity Exception. Errors were: \n\r\n\r", (current, error) => current + (" - " + error + "\n\r"));
		//			 throw new Exception (exceptionText);
		//		}
		//		AddErrors(result);
		//	}
		//	//ASDasdasdasd#12
		//	// If we got this far, something failed, redisplay form
		//	return false;
		//}

		////[HttpPost]
		////[AllowAnonymous]
		////[ValidateAntiForgeryToken]
		////public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
		////{
		////	ViewData["ReturnUrl"] = returnUrl;
		////	if (ModelState.IsValid)
		////	{
		////		var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
		////		var result = await _userManager.CreateAsync(user, model.Password);
		////		if (result.Succeeded)
		////		{
		////			_logger.LogInformation("User created a new account with password.");

		////			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		////			var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
		////			await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

		////			await _signInManager.SignInAsync(user, isPersistent: false);
		////			_logger.LogInformation("User created a new account with password.");
		////			return RedirectToLocal(returnUrl);
		////		}
		////		AddErrors(result);
		////	}

		////	// If we got this far, something failed, redisplay form
		////	return View(model);
		////}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Logout()
		//{
		//	await _signInManager.SignOutAsync();
		//	_logger.LogInformation("User logged out.");
		//	return RedirectToAction(nameof(HomeController.Index), "Home");
		//}

		//[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//public IActionResult ExternalLogin(string provider, string returnUrl = null)
		//{
		//	// Request a redirect to the external login provider.
		//	var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
		//	var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
		//	return Challenge(properties, provider);
		//}

		//[HttpGet]
		//[AllowAnonymous]
		//public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
		//{
		//	if (remoteError != null)
		//	{
		//		ErrorMessage = $"Error from external provider: {remoteError}";
		//		return RedirectToAction(nameof(Login));
		//	}
		//	var info = await _signInManager.GetExternalLoginInfoAsync();
		//	if (info == null)
		//	{
		//		return RedirectToAction(nameof(Login));
		//	}

		//	// Sign in the user with this external login provider if the user already has a login.
		//	var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
		//	if (result.Succeeded)
		//	{
		//		_logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
		//		return RedirectToLocal(returnUrl);
		//	}
		//	if (result.IsLockedOut)
		//	{
		//		return RedirectToAction(nameof(Lockout));
		//	}
		//	else
		//	{
		//		// If the user does not have an account, then ask the user to create an account.
		//		ViewData["ReturnUrl"] = returnUrl;
		//		ViewData["LoginProvider"] = info.LoginProvider;
		//		var email = info.Principal.FindFirstValue(ClaimTypes.Email);
		//		return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
		//	}
		//}

		//[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		// Get the information about the user from the external login provider
		//		var info = await _signInManager.GetExternalLoginInfoAsync();
		//		if (info == null)
		//		{
		//			throw new ApplicationException("Error loading external login information during confirmation.");
		//		}
		//		var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
		//		var result = await _userManager.CreateAsync(user);
		//		if (result.Succeeded)
		//		{
		//			result = await _userManager.AddLoginAsync(user, info);
		//			if (result.Succeeded)
		//			{
		//				await _signInManager.SignInAsync(user, isPersistent: false);
		//				_logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
		//				return RedirectToLocal(returnUrl);
		//			}
		//		}
		//		AddErrors(result);
		//	}

		//	ViewData["ReturnUrl"] = returnUrl;
		//	return View(nameof(ExternalLogin), model);
		//}

		//[HttpGet]
		//[AllowAnonymous]
		//public async Task<IActionResult> ConfirmEmail(string userId, string code)
		//{
		//	if (userId == null || code == null)
		//	{
		//		return RedirectToAction(nameof(HomeController.Index), "Home");
		//	}
		//	var user = await _userManager.FindByIdAsync(userId);
		//	if (user == null)
		//	{
		//		throw new ApplicationException($"Unable to load user with ID '{userId}'.");
		//	}
		//	var result = await _userManager.ConfirmEmailAsync(user, code);
		//	return View(result.Succeeded ? "ConfirmEmail" : "Error");
		//}

		//[HttpGet]
		//[AllowAnonymous]
		//public IActionResult ForgotPassword()
		//{
		//	return View();
		//}

		//[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var user = await _userManager.FindByEmailAsync(model.Email);
		//		if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
		//		{
		//			// Don't reveal that the user does not exist or is not confirmed
		//			return RedirectToAction(nameof(ForgotPasswordConfirmation));
		//		}

		//		// For more information on how to enable account confirmation and password reset please
		//		// visit https://go.microsoft.com/fwlink/?LinkID=532713
		//		var code = await _userManager.GeneratePasswordResetTokenAsync(user);
		//		//var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
		//		//await _emailSender.SendEmailAsync(model.Email, "Reset Password",
		//		  // $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
		//		return RedirectToAction(nameof(ForgotPasswordConfirmation));
		//	}

		//	// If we got this far, something failed, redisplay form
		//	return View(model);
		//}

		//[HttpGet]
		//[AllowAnonymous]
		//public IActionResult ForgotPasswordConfirmation()
		//{
		//	return View();
		//}

		//[HttpGet]
		//[AllowAnonymous]
		//public IActionResult ResetPassword(string code = null)
		//{
		//	if (code == null)
		//	{
		//		throw new ApplicationException("A code must be supplied for password reset.");
		//	}
		//	var model = new ResetPasswordViewModel { Code = code };
		//	return View(model);
		//}

		//[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return View(model);
		//	}
		//	var user = await _userManager.FindByEmailAsync(model.Email);
		//	if (user == null)
		//	{
		//		// Don't reveal that the user does not exist
		//		return RedirectToAction(nameof(ResetPasswordConfirmation));
		//	}
		//	var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
		//	if (result.Succeeded)
		//	{
		//		return RedirectToAction(nameof(ResetPasswordConfirmation));
		//	}
		//	AddErrors(result);
		//	return View();
		//}

		//[HttpGet]
		//[AllowAnonymous]
		//public IActionResult ResetPasswordConfirmation()
		//{
		//	return View();
		//}


		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}

		#region Helpers

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		//private IActionResult RedirectToLocal(string returnUrl)
		//{
		//	if (Url.IsLocalUrl(returnUrl))
		//	{
		//		return Redirect(returnUrl);
		//	}
		//	else
		//	{
		//		return RedirectToAction(nameof(HomeController.Index), "Home");
		//	}
		//}

		#endregion

		#endregion
	}
}
