using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface ISession
    {
        void SetUserId(int userId);
        int? GetUserId();
        void Clear();

    }
}