using App.Comments.Common.Interfaces.Services;
using App.Comments.Common.Entities;
using App.Comments.Common.Interfaces.Repositories;

namespace App.Comments.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		IApplicationUserRepository _applicationUserRepository;
		public AuthenticationService(IApplicationUserRepository applicationUserRepository)
		{
			_applicationUserRepository = applicationUserRepository;
		}

		public ApplicationUser GetUserByUserName(string UserName)
		{
			var user = _applicationUserRepository.GetUserByUserName(UserName);
			return user != null ? user : null;
		}

		public ApplicationUser GetUserByUserNameAndEmail(string UserName, string Email)
		{
			var user = _applicationUserRepository.GetUserByUserNameAndEmail(UserName, Email);
			return user != null ? user : null;
		}

		public ApplicationUser LogIn(string UserName, string Password)
		{
			var user = _applicationUserRepository.GetUser(UserName, Password);
			return user != null ? user : null;
		}

		public bool Register(ApplicationUser ApplicationUser)
		{
			_applicationUserRepository.AddUser(ApplicationUser);
			var user = _applicationUserRepository.GetUser(ApplicationUser.UserName, ApplicationUser.Password);

			return user == null ? false : true;
		}
	}
}
