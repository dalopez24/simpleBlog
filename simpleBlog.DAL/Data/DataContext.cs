using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using simpleBlog.Model;

namespace simpleBlog.DAL.Data
{
    public class DataContext :DbContext
    {
        public DataContext() : base("simpleBlog") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
