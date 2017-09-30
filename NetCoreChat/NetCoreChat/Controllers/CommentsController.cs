using Microsoft.AspNetCore.Mvc;
using App.Comments.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace App.Comments.Web.Controllers
{
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
    }
}