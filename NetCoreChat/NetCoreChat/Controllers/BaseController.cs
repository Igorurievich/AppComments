using App.Comments.Common.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App.Comments.Web.Controllers
{
    public class BaseController : Controller
	{
		public BaseController()
		{

		}
		protected readonly ICommentsService _commentsService;
		protected readonly IMapper _mapper;
	}
}
