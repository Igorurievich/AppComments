using System.Collections.Generic;
using App.Comments.Common.Entities;

namespace App.Comments.Common.Interfaces.Services
{
    public interface ICommentsService
    {
        IEnumerable<Comment> GetAllComments();

        Comment GetCommentByUserName(string UserName);
    }
}
