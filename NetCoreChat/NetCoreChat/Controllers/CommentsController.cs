using Microsoft.AspNetCore.Mvc;
using App.Comments.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using App.Comments.Common.Interfaces.Services;

namespace App.Comments.Web.Controllers
{
	public class CommentsController : Controller
	{
		ICommentsService _commentsService;
		public CommentsController(ICommentsService commentsService)
		{
			_commentsService = commentsService;
		}

		[HttpGet]
		public IEnumerable<Comment> GetAllComments()
		{
			return _commentsService.GetAllComments().AsQueryable();
		}

		[HttpGet]
		public Comment GetFirstComment()
		{
			return _commentsService.GetAllComments().AsQueryable().FirstOrDefault();
		}
	}
}
