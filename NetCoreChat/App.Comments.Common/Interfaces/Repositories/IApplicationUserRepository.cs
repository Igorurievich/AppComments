﻿using App.Comments.Common.Entities;
using System.Collections.Generic;

namespace App.Comments.Common.Interfaces.Repositories
{
    public interface IApplicationUserRepository
    {
		void AddUser(ApplicationUser User);
		void UpdateUser(ApplicationUser User);
		void DeleteUser(ApplicationUser User);
		ApplicationUser GetUser(string UserName, string Password);
		ApplicationUser GetUserByUserNameAndEmail(string UserName, string Email);
		ApplicationUser GetUserByUserName(string UserName);
		IEnumerable<ApplicationUser> GetAll();
	}
}
