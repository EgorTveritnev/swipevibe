using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface ISession
    {
        string Create(int userId);          
        void Delete(string token);
        int? GetUserId(string token);
    }
}