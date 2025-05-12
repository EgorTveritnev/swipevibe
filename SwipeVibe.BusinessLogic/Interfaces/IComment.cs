using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SwipeVibe.Domain.Entities.Comment;

namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface IComment
    {
        void AddComment(Comment comment);
        IEnumerable<Comment> GetCommentsByVideoId(int videoId);
        void DeleteComment(int commentId);
    }
}
