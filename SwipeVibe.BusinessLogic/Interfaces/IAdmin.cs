using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.Domain.Enums;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        IReadOnlyList<UserReturn> AllUsers();
        void Block(int userId);
        void Unblock(int userId);
        void SetRole(int userId, Role newRole);
    }
}

