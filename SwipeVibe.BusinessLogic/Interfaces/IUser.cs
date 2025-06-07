using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UserReturn ById(int id);

        void Update(int id, UserUpdate updateData);
        void UpdateAvatar(int userId, string avatarUrl);

    }
}