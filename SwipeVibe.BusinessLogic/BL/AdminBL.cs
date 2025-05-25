using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.BusinessLogic.Core;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;
using SwipeVibe.Domain.Enums;

namespace SwipeVibe.BusinessLogic.BL
{
    public class AdminBL : AdminApi, IAdmin
    {
        public IReadOnlyList<UserReturn> AllUsers()
        {
            return base.GetAllUsersAction();
        }

        public void Block(int id)
        {
            var user = base.ByIdAction(id);
            if (user == null)
                throw new System.Exception("User not found.");
            if (user.Role == Role.SuperAdmin.ToString())
                throw new System.Exception("Cannot block SuperAdmin.");
            if (user.IsBlocked)
                throw new System.Exception("User already blocked.");

            base.BlockAction(id);
        }

        public void Unblock(int id)
        {
            var user = base.ByIdAction(id);
            if (user == null)
                throw new System.Exception("User not found.");
            if (!user.IsBlocked)
                throw new System.Exception("User is not blocked.");

            base.UnblockAction(id);
        }

        public void SetRole(int id, Role role)
        {
            var user = base.ByIdAction(id);
            if (user == null)
                throw new System.Exception("User not found.");
            if (user.Role == Role.SuperAdmin.ToString())
                throw new System.Exception("Cannot change SuperAdmin role.");

            base.SetRoleAction(id, role);
        }
    }
}
