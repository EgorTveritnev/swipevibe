using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        IEnumerable<UserReturn> GetAllUsers();

        void Block(int userId);
        void Unblock(int userId);

        void ChangeRole(int userId, Role newRole);
    }
}
