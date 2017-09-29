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

            var comments = new Comment[]
            {
                new Comment{Title="Hello World 1 title", Description = "Hello World 1 description"},
                new Comment{Title="Hello World 2 title", Description = "Hello World 2 description"},
                new Comment{Title="Hello World 3 title", Description = "Hello World 3 description"},
            };

            foreach (var comment in comments)
            {
                context.Comments.Add(comment);
            }
            context.SaveChanges();


            ApplicationUser user = new ApplicationUser();
            user.Comments = new List<Comment>();
            user.Comments.Add(comments.FirstOrDefault());

            context.SaveChanges();
        }
    }
}
