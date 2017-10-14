using App.Comments.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            context.Users.Add(user);

            user.Comments = new Comment[]
            {
                new Comment{Title="Hello World 1 title", Description = "Hello World 1 description", ApplicationUser = user},
                new Comment{Title="Hello World 2 title", Description = "Hello World 2 description", ApplicationUser = user},
                new Comment{Title="Hello World 3 title", Description = "Hello World 3 description", ApplicationUser = user},
            };

            foreach (var comment in user.Comments)
            {
                context.Comments.Add(comment);
            }

            context.SaveChanges();
        }
    }
}
