using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwipeVibe.Domain.Entities.Video;

namespace SwipeVibe.Web.Models
{
    public class AdminDashboardViewModel
    {
        public int RegisteredUsersCount { get; set; }
        public int TotalVideosCount { get; set; }
        public int ActiveUsersCount { get; set; }
        public int BlockedUsersCount { get; set; }
        public int TodayNewUsersCount { get; set; }
        public int TodayNewVideosCount { get; set; }
        public List<Video> LatestVideos { get; set; }
        
        public AdminDashboardViewModel()
        {
            LatestVideos = new List<Video>();
        }
    }
}
