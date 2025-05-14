using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.BusinessLogic.Core;   
using SwipeVibe.BusinessLogic.BL;

namespace SwipeVibe.BusinessLogic
{
    public class BussinesLogic
    {
        public readonly IUser User;
        public readonly IAdmin Admin;

        public BussinesLogic()
        {
            var mapper = MapperBootstrap.Mapper;
            var repo = new UserRepository();
            var session = new SessionBL();
            User = new Core.UserApi(repo, session, mapper);
            Admin = new Core.AdminApi(repo, mapper);
        }
    }
}
