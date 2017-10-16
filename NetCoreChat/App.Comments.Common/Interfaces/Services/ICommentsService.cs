using System.Collections.Generic;
using App.Comments.Common.Entities;
using System;

namespace App.Comments.Common.Interfaces.Services
{
    public interface ICommentsService
    {
        IEnumerable<CommentDto> GetAllComments();

        CommentDto GetCommentByUserName(string UserName);

		void AddNewComment(CommentDto comment);
    }
}
