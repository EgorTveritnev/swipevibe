using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SwipeVibe.BusinessLogic.DBModel;
using SwipeVibe.Domain.Entities.User;
using SwipeVibe.Domain.Enums;

namespace SwipeVibe.BusinessLogic.Core
{
    public class AdminApi
{
    protected IReadOnlyList<UserReturn> GetAllUsersAction()
    {
        using (var db = new UserContext())
        {
            return db.Users.Select(u => new UserReturn
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                AvatarUrl = u.AvatarUrl,
                RegisteredDate = u.CreatedAt,
                Role = u.Role.ToString(),
                IsBlocked = u.IsBlocked
            }).ToList();
        }
    }

    protected UserReturn ByIdAction(int id)
    {
        using (var db = new UserContext())
        {
            var u = db.Users.FirstOrDefault(x => x.Id == id);
            if (u == null) return null;

            return new UserReturn
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                AvatarUrl = u.AvatarUrl,
                RegisteredDate = u.CreatedAt,
                Role = u.Role.ToString(),
                IsBlocked = u.IsBlocked
            };
        }
    }

    protected void BlockAction(int id)
    {
        using (var db = new UserContext())
        {
            var u = db.Users.FirstOrDefault(x => x.Id == id);
            if (u != null)
            {
                u.IsBlocked = true;
                db.SaveChanges();
            }
        }
    }

    protected void UnblockAction(int id)
    {
        using (var db = new UserContext())
        {
            var u = db.Users.FirstOrDefault(x => x.Id == id);
            if (u != null)
            {
                u.IsBlocked = false;
                db.SaveChanges();
            }
        }
    }

    protected void SetRoleAction(int id, Role newRole)
    {
        using (var db = new UserContext())
        {
            var u = db.Users.FirstOrDefault(x => x.Id == id);
            if (u != null)
            {
                u.Role = newRole;
                db.SaveChanges();
            }
        }
    }
}
}