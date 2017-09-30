using App.Comments.Common.Entities;
using System.Collections.Generic;

namespace App.Comments.Common.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);

        Comment GetCommentById(uint id);
        IEnumerable<Comment> GetAll();
    }
}
