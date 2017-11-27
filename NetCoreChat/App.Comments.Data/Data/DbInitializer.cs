using App.Comments.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Comments.Data.Data
{
    public static class DbInitializer
    {

        public static void Initialize(CommentsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Comments.Any())
            {
                return;
            }

            ApplicationUser user = new ApplicationUser();
			user.UserName = "admin";
			user.Password = "admin";
			user.Email = "admin@example.com";

			context.Users.Add(user);
			user.Comments = new List<Comment>();
			Random random = new Random();
			
			for (int i = 0; i < 50; i++)
			{
				user.Comments.Add(new Comment {
					Title = "Hello World " + i + " title",
					CommentText = "Hello World " + i + " description",
					ApplicationUser = user,
					CommentData = new CommentData()
					{
						Likes = random.Next(0, 50),
						Dislikes = random.Next(0, 50),
						CommentDescription = $"Random comment description"
					}
				});
			}

            foreach (var comment in user.Comments)
            {
                context.Comments.Add(comment);
            }

			context.SaveChanges();
        }
    }
}
