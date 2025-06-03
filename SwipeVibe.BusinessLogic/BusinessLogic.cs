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
        public IAdmin GetAdminBL()
        {
            return new AdminBL();
        }
        public IUser GetUserBL()
        {
            return new UserBL();
        }
        public ISession GetSessionBL()
        {
            return new UserBL();
        }

        public IVideo GetVideoBL()
        {
            return new VideoBL();
        }
        public IComment GetCommentBL()
        {
            return new CommentBL();
        }
    }
}