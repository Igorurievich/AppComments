using App.Comments.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using App.Comments.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Comments.Data.Repositories
{
	public class UserRepository : IApplicationUserRepository
	{
		private readonly CommentsContext _dbContext;

		public UserRepository(CommentsContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void AddUser(ApplicationUser User)
		{
			_dbContext.Users.Add(User);
			_dbContext.SaveChanges();
		}

		public void DeleteUser(ApplicationUser User)
		{
			_dbContext.Users.Remove(User);
			_dbContext.SaveChanges();
		}

		public IEnumerable<ApplicationUser> GetAll()
		{
			return _dbContext.Users
			.Include(comment => comment.Comments)
			.AsEnumerable();
		}

		public ApplicationUser GetUser(string UserName, string Password)
		{
			return _dbContext.Users
				.FirstOrDefault(x => x.UserName == UserName && x.Password == Password);
		}

		public ApplicationUser GetUserByUserNameAndEmail(string UserName, string Email)
		{
			return _dbContext.Users.FirstOrDefault(user => user.UserName == UserName && user.Email == Email);
		}

		public void UpdateUser(ApplicationUser User)
		{
			_dbContext.Users.Update(User);
			_dbContext.SaveChanges();
		}
	}
}
