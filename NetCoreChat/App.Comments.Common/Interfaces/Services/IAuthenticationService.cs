using App.Comments.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Comments.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        ApplicationUser GetUser(string UserName, string Password);
    }
}
