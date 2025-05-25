using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=SwipeVibe") { }
        public DbSet<User> Users { get; set; }
    }
}