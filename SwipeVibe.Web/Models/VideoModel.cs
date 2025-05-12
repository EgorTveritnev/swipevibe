using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SwipeVibe.Web.Models
{
    public class VideoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название видео")]
        [Display(Name = "Название")]
        [StringLength(100, ErrorMessage = "Название не может превышать 100 символов")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        [StringLength(500, ErrorMessage = "Описание не может превышать 500 символов")]
        public string Description { get; set; }

        [Display(Name = "Хештеги")]
        [StringLength(200, ErrorMessage = "Хештеги не могут превышать 200 символов")]
        public string Hashtags { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите файл видео")]
        [Display(Name = "Видеофайл")]
        public HttpPostedFileBase VideoFile { get; set; }

        [Display(Name = "Обложка видео")]
        public HttpPostedFileBase ThumbnailImage { get; set; }

        public string VideoPath { get; set; }

        public string ThumbnailPath { get; set; }

        public DateTime UploadDate { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }

        public int SharesCount { get; set; }
    }
}