using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DataContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserWeightHistory> UserWeightHistorys { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed default roles
            string adminRoleName = "admin";
            string userRoleName = "user";
            Role adminRole = new() { Id = 1, RoleName = adminRoleName };
            Role userRole = new() { Id = 2, RoleName = userRoleName };

            // Seed default admin
            string adminLogin = "admin";
            string adminPassword = "admin";
            User adminUser = new() { Id = 1, Login = adminLogin, Password = adminPassword, RoleId = adminRole.Id };
            //TODO ADD admin parameter(weight history) here

            // Seed default Products
            //var defaultProducts = new Product[]
            //{
            //    new Product(){ Id = 1, ProductName = "Water"},
            //    new Product(){ Id = 2, ProductName = "Tea"},
            //    new Product(){ Id = 3, ProductName = "Coffee"},
            //};

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            //modelBuilder.Entity<Product>().HasData(defaultProducts);
            base.OnModelCreating(modelBuilder);
        }
    }
}
