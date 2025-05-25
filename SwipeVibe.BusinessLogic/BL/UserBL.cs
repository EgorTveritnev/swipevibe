using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.BusinessLogic.Core;
using SwipeVibe.BusinessLogic.DBModel;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;


namespace SwipeVibe.BusinessLogic.BL
{
    public class UserBL : UserApi, IUser, ISession
    {
        public UserReturn ById(int id)
        {
            var u = base.ByIdAction(id);
            if (u == null)
                throw new System.Exception("User not found.");
            if (u.IsBlocked)
                throw new System.Exception("User is blocked.");
            return u;
        }

        public void Update(int id, UserUpdate upd)
        {
            if (!string.IsNullOrWhiteSpace(upd.Email) && !upd.Email.Contains("@"))
                throw new System.Exception("Invalid email.");
            base.UpdateAction(id, upd);
        }

        public UserLoginResult Login(UserLoginData creds)
        {
            var res = base.LoginAction(creds);
            if (!res.Status)
                throw new System.Exception(res.StatusMsg);
            if (res.UserInfo != null && res.UserInfo.IsBlocked)
                throw new System.Exception("User is blocked.");
            return res;
        }

        public UserRegisterResult Register(UserRegisterData dto)
        {
            if (dto.Email.EndsWith("@baddomain.com"))
                throw new System.Exception("Registration with this domain is forbidden.");

            var res = base.RegisterAction(dto);
            if (!res.Status)
                throw new System.Exception(res.StatusMsg);

            return res;
        }

        public void Logout(int userId)
        {
            base.LogoutAction(userId);
        }
    }
}