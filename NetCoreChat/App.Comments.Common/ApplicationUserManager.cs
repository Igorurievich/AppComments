using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using App.Comments.Common.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.Comments.Common
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		
		public ApplicationUserManager(IUserStore<ApplicationUser> store, 
			IOptions<IdentityOptions> optionsAccessor, 
			IPasswordHasher<ApplicationUser> passwordHasher, 
			IEnumerable<IUserValidator<ApplicationUser>> userValidators, 
			IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, 
			ILookupNormalizer keyNormalizer, 
			IdentityErrorDescriber errors, 
			IServiceProvider services, 
			ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
		{
			
		}
	}
}
