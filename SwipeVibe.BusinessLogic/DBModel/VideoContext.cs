using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SwipeVibe.Domain.Entities.Video;

namespace SwipeVibe.BusinessLogic.DBModel
{
    public class VideoContext : DbContext
    {
        public VideoContext() : base("name=SwipeVibe") { }

        public DbSet<Video> Videos { get; set; }
    }
}