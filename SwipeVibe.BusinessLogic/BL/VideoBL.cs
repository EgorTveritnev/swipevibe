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
        Username = "coolcontent",
        AvatarUrl = "https://i.pravatar.cc/150?img=2",
        Description = "ПРИРОДА :))) #demo #cdn",
        GradientCss = "linear-gradient(135deg, #00F5A0, #00D9F5)",
        LikesCount = 45100,
        CommentsCount = 3700,
        SharesCount = 8200,
        VideoUrl = "https://samplelib.com/lib/preview/mp4/sample-5s.mp4",
        UploadDate = DateTime.UtcNow
    },

    new Video
    {
        Id = 2,
        Username = "internetstar",
        AvatarUrl = "https://i.pravatar.cc/150?img=4",
        Description = "Удивительно как красиво! #viral #wow",
        GradientCss = "linear-gradient(135deg, #8E2DE2, #4A00E0)",
        LikesCount = 154000,
        CommentsCount = 9100,
        SharesCount = 23200,
        VideoUrl = "	https://samplelib.com/lib/preview/mp4/sample-10s.mp4",
        UploadDate = DateTime.UtcNow
    },
    new Video
    {
        Id = 3,
        Username = "externalhero",
        AvatarUrl = "https://i.pravatar.cc/150?img=6",
        Description = " Больше природы #cloud",
        GradientCss = "linear-gradient(135deg, #FFDEE9, #B5FFFC)",
        LikesCount = 62000,
        CommentsCount = 7300,
        SharesCount = 9400,
        VideoUrl = "https://samplelib.com/lib/preview/mp4/sample-15s.mp4",
        UploadDate = DateTime.UtcNow
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