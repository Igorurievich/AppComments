using System.Collections.Generic;
using App.Comments.Common.Entities;
using App.Comments.Common.Interfaces.Repositories;
using App.Comments.Common.Interfaces.Services;

namespace App.Comments.Common.Services
{
    public class CommentsService : ICommentsService
    {
        public CommentsService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        private readonly ICommentRepository _commentRepository;
        
        public IEnumerable<Comment> GetAllComments()
        {
            return _commentRepository.GetAll();        
        }

        public Comment GetCommentByUserName(string UserName)
        {
            return _commentRepository.GetCommentByUserName(UserName);
        }
    }
}