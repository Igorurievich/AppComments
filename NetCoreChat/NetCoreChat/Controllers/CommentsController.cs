using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using App.Comments.Common.Interfaces.Services;
using AutoMapper;

namespace App.Comments.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	public class CommentsController
	{
		private readonly ICommentsService _commentsService;
		private readonly IMapper _mapper;
		public CommentsController(
			ICommentsService commentsService,
			IMapper mapper)
		{
			_commentsService = commentsService;
			_mapper = mapper;
		}

		[HttpGet]
		public IEnumerable<CommentDto> GetAllComments()
		{
			return _commentsService.GetAllComments().AsEnumerable();
		}

		[HttpGet]
		public CommentDto GetFirstComment()
		{
			return _commentsService.GetAllComments().FirstOrDefault();
		}

		[HttpPost]
		public void NewComment([FromBody]CommentDto comment)
		{
			_commentsService.AddNewComment(comment);
		}
	}
}
