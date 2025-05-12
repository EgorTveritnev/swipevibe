using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeVibe.Domain.Entities.Comment
{
    public class Comment
    {
        public const int MaxTextLength = 50;   

        public int Id { get; set; }
        public int VideoId { get; set; }
        public string Text { get; set; }       
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}