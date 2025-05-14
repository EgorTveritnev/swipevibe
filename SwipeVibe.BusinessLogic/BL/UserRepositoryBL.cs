using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;
using System.Collections.Generic;
using System.Linq;
using System;

public class UserRepositoryBL : IUserRepository
{
    private readonly SwipeVibeDbContext _db = new SwipeVibeDbContext();

    public IEnumerable<User> All() => _db.Users.ToList();

    public User ById(int id) => _db.Users.Find(id);

    public User ByEmail(string email) =>
        _db.Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    public void Add(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public void Update(User user)
    {
        _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = _db.Users.Find(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }

    public User ByUsername(string username) =>
        _db.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
}
