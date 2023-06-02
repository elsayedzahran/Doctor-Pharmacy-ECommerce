using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ProjectTest.Models
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity, RoleEntity, string>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ } 
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<SpecializeEntity> Specializes { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<GovernateEntity> Governates { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
    }
}
