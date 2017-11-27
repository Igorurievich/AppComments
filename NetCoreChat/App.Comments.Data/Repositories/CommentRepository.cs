using System.Collections.Generic;
using App.Comments.Common.Entities;
using App.Comments.Common.Interfaces.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace App.Comments.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
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

		public void AddComments(IEnumerable<Comment> comments)
		{
			_dbContext.Comments.AddRange(comments);
			_dbContext.SaveChanges();
		}

		public void DeleteComment(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }

		public IEnumerable<Comment> GetLast50Comments()
        {
			return _dbContext.Comments.OrderByDescending(p => p.CommentId).Take(50)
			.Include(appUser => appUser.ApplicationUser)
			.AsEnumerable();
		}

		public IQueryable GetAll()
		{
			var comments = _dbContext.Comments;
			var commentsData = _dbContext.CommentsData;
			var users = _dbContext.Users;

			var data = from c in comments
					   join cd in commentsData on c.CommentId equals cd.CommentDataId
					   join u in users on c.ApplicationUser.UserId equals u.UserId
					   where c.CommentText != null && c.Title != null && c.CommentId > 0
					   select new
					   {
						   Autor = u.UserName,
						   CommentTitle = c.Title,
						   CommentText = c.CommentText,
						   PostTime = c.PostTime,
						   Likes = cd.Likes,
						   Dislikes = cd.Dislikes
					   };
			return data;
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

		public void DeleteComments(IEnumerable<Comment> commentsForDelete)
		{
			_dbContext.RemoveRange(commentsForDelete);
			_dbContext.SaveChanges();
		}

		public void DeleteAllComments()
		{
			_dbContext.Comments.RemoveRange(_dbContext.Comments);
			_dbContext.SaveChanges();
		}
	}
}
