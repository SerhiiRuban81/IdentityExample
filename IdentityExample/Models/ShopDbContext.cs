using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample.Models
{
    public class ShopDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
