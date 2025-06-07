using SwipeVibe.BusinessLogic.DBModel;
using SwipeVibe.Domain.Entities.Video;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeVibe.BusinessLogic.Core
{
    public class VideoApi
    {
        protected IEnumerable<Video> GetAllAction()
        {
            using (var db = new VideoContext())
                return db.Videos
                         .OrderByDescending(v => v.UploadDateUtc)
                         .ToList();
        }

        protected Video GetByIdAction(int id)
        {
            using (var db = new VideoContext())
                return db.Videos.FirstOrDefault(v => v.Id == id);
        }

        protected void AddAction(Video video)
        {
            using (var db = new VideoContext())
            {
                db.Videos.Add(video);
                db.SaveChanges();
            }
        }

        protected void UpdateAction(Video video)
        {
            using (var db = new VideoContext())
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        protected void DeleteAction(int id)
        {
            using (var db = new VideoContext())
            {
                var v = db.Videos.Find(id);
                if (v == null) return;
                db.Videos.Remove(v);
                db.SaveChanges();
            }
        }

        protected void IncrementAction(int id, System.Action<Video> mutation)
        {
            using (var db = new VideoContext())
            {
                var v = db.Videos.Find(id);
                if (v == null) return;
                mutation(v);
                db.SaveChanges();
            }
        }
    }
}