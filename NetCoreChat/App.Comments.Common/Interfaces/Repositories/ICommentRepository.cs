using App.Comments.Common.Entities;
using System.Collections.Generic;
using System.Linq;

namespace App.Comments.Common.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
		void AddComments(IEnumerable<Comment> comments);
		void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
        Comment GetCommentByUserName(string UserName);
        IQueryable GetAll();
		IEnumerable<Comment> GetLast50Comments();
		void DeleteComments(IEnumerable<Comment> commentForDelete);
		void DeleteAllComments();
	}
}
