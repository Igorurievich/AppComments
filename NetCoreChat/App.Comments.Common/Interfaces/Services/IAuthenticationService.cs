using App.Comments.Common.Entities;

namespace App.Comments.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
		bool Register(ApplicationUser ApplicationUser);
		ApplicationUser LogIn(string UserName, string Password);
		ApplicationUser GetUserByUserNameAndEmail(string UserName, string Email);
	}
}
