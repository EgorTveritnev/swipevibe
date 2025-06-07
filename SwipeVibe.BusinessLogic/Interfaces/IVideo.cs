using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.Domain.Entities.Video;

namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface IVideo
    {
        IEnumerable<Video> GetAll();
        Video GetById(int id);
        void Add(Video video);
        void Update(Video video);
        void Delete(int id);
        void IncrementLikes(int id);
        void IncrementComments(int id);
        void IncrementShares(int id);
    }
}