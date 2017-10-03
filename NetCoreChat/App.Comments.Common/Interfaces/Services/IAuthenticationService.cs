using App.Comments.Common.Entities;

namespace App.Comments.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        ApplicationUser GetUser(string UserName, string Password);
    }
}
