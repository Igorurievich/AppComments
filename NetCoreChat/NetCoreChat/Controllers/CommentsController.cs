using Microsoft.AspNetCore.Mvc;
using App.Comments.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using App.Comments.Common.Interfaces.Services;
using AutoMapper;

namespace App.Comments.Web.Controllers
{

	public class CommentsController : Controller
	{
		private readonly ICommentsService _commentsService;
		private readonly IMapper _mapper;
		public CommentsController(ICommentsService commentsService, IMapper mapper)
		{
			_commentsService = commentsService;
			_mapper = mapper;
		}

		[HttpGet]
		public IEnumerable<CommentDto> GetAllComments()
		{
			var result = _commentsService.GetAllComments().ToList();
			return _mapper.Map<List<Comment>, List<CommentDto>>(result);
		}

		[HttpGet]
		public CommentDto GetFirstComment()
		{
			var user = _commentsService.GetAllComments().AsQueryable().FirstOrDefault();
			return _mapper.Map<Comment, CommentDto>(user);
		}
	}
}
