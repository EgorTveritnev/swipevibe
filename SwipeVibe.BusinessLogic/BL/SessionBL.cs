using SwipeVibe.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeVibe.BusinessLogic
{
    public class SessionBL : ISession
    {
        // временно просто хранит int UserId в памяти
        private readonly Dictionary<string, int> _sessions = new Dictionary<string, int>();
        public string Create(int userId)
        {
            var token = Guid.NewGuid().ToString();
            _sessions[token] = userId;
            return token;
        }
        public void Delete(string token)
        {
            _sessions.Remove(token);
        }

        public int? GetUserId(string token)
        {
            int id;
            return _sessions.TryGetValue(token, out id) ? (int?)id : null;

        }
    }
}