using App.Comments.Common.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace App.Comments.Web
{
	public class CommentsPublisher : Hub
    {
		private readonly ICommentsService _commentsService;

		public CommentsPublisher(ICommentsService commentsService)
		{
			_commentsService = commentsService;
		}

		public Task Send(CommentDto newComment)
		{
			_commentsService.AddNewComment(newComment);
			return Clients.All.InvokeAsync("Send", newComment);
		}
	}
}
