using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.Video;

namespace SwipeVibe.BusinessLogic.BL
{
    public class VideoBL : IVideo
    {
        private static readonly List<Video> _videos = new List<Video>
        {
            new Video
            {
                Id = 1,
                Username = "user123",
                AvatarUrl = "https://i.pravatar.cc/150?img=1",
                Description = "Это мое первое видео в SwipeVibe! #swipevibe #trending #viral",
                GradientCss = "linear-gradient(135deg, #FF6CAB, #7366FF)",
                LikesCount = 12_500, CommentsCount = 1_200, SharesCount = 3_400
            },
            new Video
            {
                Id = 2,
                Username = "coolcontent",
                AvatarUrl = "https://i.pravatar.cc/150?img=2",
                Description = "Смотрите это невероятное видео! #amazing #swipevibe #content",
                GradientCss = "linear-gradient(135deg, #00F5A0, #00D9F5)",
                LikesCount = 45_100, CommentsCount = 3_700, SharesCount = 8_200
            },
            new Video
            {
                Id = 3,
                Username = "creativeminds",
                AvatarUrl = "https://i.pravatar.cc/150?img=3",
                Description = "Новый челлендж! Попробуйте сами! #challenge #swipevibe #new",
                GradientCss = "linear-gradient(135deg, #FF8A00, #FF0080)",
                LikesCount = 78_300, CommentsCount = 5_600, SharesCount = 12_900
            }
        };

        public IEnumerable<Video> GetAll() => _videos;
        public Video GetById(int id) => _videos.FirstOrDefault(v => v.Id == id);

        public void Add(Video video)
        {
            video.Id = _videos.Any() ? _videos.Max(v => v.Id) + 1 : 1;
            _videos.Add(video);
        }
        public void Delete(int id) => _videos.RemoveAll(v => v.Id == id);

        public void IncrementLikes(int id) => GetById(id)?.Let(v => v.LikesCount++);
        public void IncrementComments(int id) => GetById(id)?.Let(v => v.CommentsCount++);
        public void IncrementShares(int id) => GetById(id)?.Let(v => v.SharesCount++);
    }

    static class VideoExt
    {
        public static void Let<T>(this T obj, System.Action<T> act) where T : class
        {
            if (obj != null) act(obj);
        }
    }
}