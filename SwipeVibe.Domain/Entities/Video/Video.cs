using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeVibe.Domain.Entities.Video
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public string FileUrl { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public int DurationSec { get; set; }

        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SharesCount { get; set; }

        public DateTime UploadDateUtc { get; set; }
    }
}