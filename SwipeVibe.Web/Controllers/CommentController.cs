using System;
using System.Linq;
using System.Web.Mvc;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.BusinessLogic.BL;
using SwipeVibe.Domain.Entities.Comment;

namespace SwipeVibe.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly IComment _commentService;

        public CommentController()
        {
            _commentService = new CommentBL();
        }

        [HttpGet]
        public ActionResult List(int videoId)
        {
            var comments = _commentService
                .GetCommentsByVideoId(videoId)
                .OrderBy(c => c.CreatedAt);

            return PartialView("~/Views/Home/_CommentsList.cshtml", comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int videoId, string text)
        {
            var comment = new Comment
            {
                VideoId = videoId,
                Username = User.Identity.IsAuthenticated ? User.Identity.Name : "Гость",
                Text = text?.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                _commentService.AddComment(comment);
            }
            catch (ArgumentException ex)   
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }

            var comments = _commentService.GetCommentsByVideoId(videoId).OrderBy(c => c.CreatedAt);
            return PartialView("~/Views/Home/_CommentsList.cshtml", comments);
        }
    }
}
