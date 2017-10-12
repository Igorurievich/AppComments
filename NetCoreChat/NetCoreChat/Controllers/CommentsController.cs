using Microsoft.AspNetCore.Mvc;
using App.Comments.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using App.Comments.Common.Entities;
using System.Collections.Generic;
using System.Linq;

namespace App.Comments.Web.Controllers
{

    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        ICommentRepository _commentsRepository;

        public CommentsController(ICommentRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        [Authorize]
        public IActionResult All()
        {
            return View(_commentsRepository.GetAll());
        }

        [HttpGet]
        public IEnumerable<string> GetAllTitles()
        {
            return _commentsRepository.GetAll().Select(x => x.Title);
        }
    }
}
