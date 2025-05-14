using SwipeVibe.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeVibe.BusinessLogic.BL
{
    public class SessionBL : ISession
    {
        private int? _currentUserId;

        public void SetUserId(int userId)
        {
            _currentUserId = userId;
        }

        public int? GetUserId()
        {
            return _currentUserId;
        }

        public void Clear()
        {
            _currentUserId = null;
        }
    }
}