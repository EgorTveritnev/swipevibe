using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.BusinessLogic.Core;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.Video;

namespace SwipeVibe.BusinessLogic.BL
{
    public class VideoBL : VideoApi, IVideo
    {
        public IEnumerable<Video> GetAll() => GetAllAction();
        public Video GetById(int id) => GetByIdAction(id);

        public void Add(Video video)
        {
            if (video == null) throw new System.ArgumentNullException(nameof(video));
            if (string.IsNullOrWhiteSpace(video.FileUrl))
                throw new System.ArgumentException("FileUrl is required");

            AddAction(video);
        }

        public void Update(Video video) => UpdateAction(video);
        public void Delete(int id) => DeleteAction(id);

        public void IncrementLikes(int id) => IncrementAction(id, v => v.LikesCount++);
        public void IncrementComments(int id) => IncrementAction(id, v => v.CommentsCount++);
        public void IncrementShares(int id) => IncrementAction(id, v => v.SharesCount++);
    }
}