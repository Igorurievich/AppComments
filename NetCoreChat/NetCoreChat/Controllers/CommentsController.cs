using Microsoft.AspNetCore.Mvc;
using App.Comments.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using App.Comments.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using App.Comments.Common.Interfaces.Services;

namespace App.Comments.Web.Controllers
{

	[Route("api/[controller]")]
	public class CommentsController : Controller
	{
		ICommentsService _commentsService;

		public CommentsController(ICommentsService commentsService)
		{
			_commentsService = commentsService;
		}

		[Authorize]
		public IActionResult All()
		{
			return View(_commentsService.GetAllComments());
		}

		[HttpGet]
		public List<Comment> GetAllTitles()
		{
			var test = _commentsService.GetAllComments().ToList();
			return test;
		}
	}
}
