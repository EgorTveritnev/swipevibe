using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SwipeVibe.Web.Models
{
    public class VideoViewModel
    {
        public int Id { get; set; }
        public string FileUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationSec { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SharesCount { get; set; }
        public DateTime UploadDateUtc { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatarUrl { get; set; }

        [Required(ErrorMessage = "Выберите видеофайл")]
        public HttpPostedFileBase File { get; set; }

        [Required(ErrorMessage = "Введите название")]
        [StringLength(120)]
        public string UploadTitle { get; set; }

        [StringLength(1000)]
        public string UploadDescription { get; set; }
    }
}