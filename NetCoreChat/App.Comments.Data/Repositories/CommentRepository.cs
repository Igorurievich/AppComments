using System.Collections.Generic;
using App.Comments.Common.Entities;
using App.Comments.Common.Interfaces.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Comments.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
		public Action<string> Log { get; set; }

		private readonly CommentsContext _dbContext;

        public CommentRepository(CommentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddComment(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Comment> GetAll()
        {
            return _dbContext.Comments
            .Include(appUser => appUser.ApplicationUser)
            .AsEnumerable();
        }

        public Comment GetCommentByUserName(string UserName)
        {
            return _dbContext.Comments.FirstOrDefault(x => x.ApplicationUser.UserName == UserName);
        }

        public void UpdateComment(Comment comment)
        {
            _dbContext.Comments.Update(comment);
            _dbContext.SaveChanges();
        }
    }
}
