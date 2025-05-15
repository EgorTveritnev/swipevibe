using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeVibe.Domain.Entities.Video
{
    public class Video
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
        public string GradientCss { get; set; }
        public DateTime UploadDate { get; set; }
        public string VideoUrl { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SharesCount { get; set; }
    }
}