using System;
using System.Data.Entity;
using System.Linq;

namespace StoreProject.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext():base("name=Store")
        {
            
        }
        public DbSet<Store> stores { get; set; }
        public DbSet<Space> spaces { get; set; }
        public DbSet<Product> products { get; set; }
    }
}