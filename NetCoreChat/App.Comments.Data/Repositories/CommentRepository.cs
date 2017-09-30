using System.Collections.Generic;
using App.Comments.Common.Entities;
using App.Comments.Common.Interfaces.Repositories;
using System.Linq;

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

        public void DeleteComment(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Comment> GetAll()
        {
            return _dbContext.Comments.AsEnumerable();
        }

        public Comment GetCommentById(uint id)
        {
            return _dbContext.Comments.FirstOrDefault(x => x.ID == id);
        }

        public void UpdateComment(Comment comment)
        {
            _dbContext.Comments.Update(comment);
            _dbContext.SaveChanges();
        }
    }
}
