using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ProjectTest
{
    public static class Constants
    {
        public const string TokenKey = "qpdwqwidjqwiodjqowndqiwodbuxqohunqwdqwdqwdqw";
    }
}

//public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//{
//    private readonly IHttpContextAccessor _httpContextAccessor;

//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
//        : base(options)
//    {
//        _httpContextAccessor = httpContextAccessor;
//    }

//    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//    {
//        var currentUser = _httpContextAccessor.HttpContext.User;
//        var userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
//        var userName = currentUser.Identity.Name;

//        // Get all Added/Modified entities that inherit from IdentityUser
//        var entities = ChangeTracker
//            .Entries()
//            .Where(x => x.Entity is IdentityUser && (x.State == EntityState.Added || x.State == EntityState.Modified));

//        // Update the UserName property for each added/modified entity
//        foreach (var entity in entities)
//        {
//            var user = (IdentityUser)entity.Entity;
//            user.UserName = userName;
//        }

//        // Call the base SaveChangesAsync method to save changes to the database
//        return await base.SaveChangesAsync(cancellationToken);
//    }
//}