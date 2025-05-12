using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.Comment;

namespace SwipeVibe.BusinessLogic.BL
{
    public class CommentBL : IComment
    {
        private static readonly List<Comment> _comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                VideoId = 1,
                Username = "Alice",
                Text = "Отличное видео!",
                CreatedAt = DateTime.UtcNow.AddMinutes(-45)
            },
            new Comment
            {
                Id = 2,
                VideoId = 1,
                Username = "Bob",
                Text = "Очень понравилось, жду продолжений 😊",
                CreatedAt = DateTime.UtcNow.AddHours(-2)
            },
            new Comment
            {
                Id = 3,
                VideoId = 1,
                Username = "Charlie",
                Text = "Невероятные кадры!",
                CreatedAt = DateTime.UtcNow.AddMinutes(-10)
            },
            new Comment
            {
                Id = 4,
                VideoId = 1,
                Username = "Daria",
                Text = "🔥🔥🔥",
                CreatedAt = DateTime.UtcNow.AddMinutes(-5)
            },

            new Comment
            {
                Id = 1,
                VideoId = 2,
                Username = "Alice",
                Text = "It's amazing!",
                CreatedAt = DateTime.UtcNow.AddMinutes(-45)
            },
            new Comment
            {
                Id = 2,
                VideoId = 3,
                Username = "Bob",
                Text = "WOW 😊",
                CreatedAt = DateTime.UtcNow.AddHours(-2)
            },
            new Comment
            {
                Id = 3,
                VideoId = 2,
                Username = "5opka",
                Text = "OMG!",
                CreatedAt = DateTime.UtcNow.AddMinutes(-10)
            },
            new Comment
            {
                Id = 4,
                VideoId = 3,
                Username = "Oleg",
                Text = "🔥🔥🔥",
                CreatedAt = DateTime.UtcNow.AddMinutes(-5)
            }
        };

        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrWhiteSpace(comment.Text))
                throw new ArgumentException("Комментарий не может быть пустым.");

            comment.Text = comment.Text.Trim();          

            if (comment.Text.Length > Comment.MaxTextLength)
                throw new ArgumentException(
                    $"Комментарий не может превышать {Comment.MaxTextLength} символов.");

            comment.Id = _comments.Count > 0 ? _comments.Max(c => c.Id) + 1 : 1;
            comment.CreatedAt = DateTime.UtcNow;
            _comments.Add(comment);
        }

        public IEnumerable<Comment> GetCommentsByVideoId(int videoId)
        {
            return _comments.Where(c => c.VideoId == videoId)
                            .OrderBy(c => c.CreatedAt);
        }

        public void DeleteComment(int commentId)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == commentId);
            if (comment != null)
                _comments.Remove(comment);
        }
    }
}