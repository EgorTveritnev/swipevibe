using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.BusinessLogic.BL;

namespace SwipeVibe.BusinessLogic
{
    public class BusinessLogic
    {
        public IAdmin Admin { get; }
        public IUser User { get; }
        public ISession Session { get; }
        public IVideo Video { get; }
        public BusinessLogic()
        {
            var userBl = new UserBL();

            Admin = new AdminBL();
            User = userBl;
            Session = userBl;
            Video = new VideoBL();
        }
    }
}