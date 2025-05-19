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
        IEnumerable<UserReturn> GetAllUsers();
        void Block(int id);
        void Unblock(int id);
        void SetRole(int id, string role);
    }
}

