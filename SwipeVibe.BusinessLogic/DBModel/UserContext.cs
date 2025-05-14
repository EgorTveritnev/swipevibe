using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SwipeVibe.Domain.Entities.User;

public class SwipeVibeDbContext : DbContext
{
    public SwipeVibeDbContext() : base("name=SwipeVibeConnection") {}
    public DbSet<User> Users { get; set; }
}