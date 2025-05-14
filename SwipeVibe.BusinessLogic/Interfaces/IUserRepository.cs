using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> All();
        User ById(int id);
        User ByEmail(string email);
        User ByUsername(string username);
        void Add(User user);
        void Update(User user);
        void Delete(int id); 
    }
}
