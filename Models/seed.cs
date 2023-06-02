using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ProjectTest.Models
{
    public static class seed
    {
        public static async Task SeedUsers(
            UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager,
            ApplicationDbContext context)
        {
            if (userManager is null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }
            if (roleManager is null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            if (await userManager.Users.AnyAsync())
                return;

            var governates = new List<GovernateEntity>
            {
                new GovernateEntity
                {
                    Name = "gov1"
                },
                new GovernateEntity
                {
                    Name = "gov2"
                }
            };
            foreach (var gov in governates)
            {
                context.Governates.Add(gov);
                await context.SaveChangesAsync();
            }
            var categories = new List<CategoryEntity>
            {
                new CategoryEntity
                {
                    Name = "cat1"
                },
                new CategoryEntity
                {
                    Name = "cat2"
                }
            };
            foreach (var cat in categories)
            {
                context.Categories.Add(cat);
                await context.SaveChangesAsync();
            }
            var specialize = new List<SpecializeEntity>
            {
                new SpecializeEntity
                {
                    Name = "spec1"
                },
                new SpecializeEntity
                {
                    Name = "spec2"
                }
            };
            foreach (var spec in specialize)
            {
                context.Specializes.Add(spec);
                await context.SaveChangesAsync();
            }
            var cities = new List<CityEntity>
            {
                new CityEntity
                {
                    Name = "cit1",
                    GovernateId = 1
                },
                new CityEntity
                {
                    Name = "cit2",
                    GovernateId = 2
                }
            };
            foreach (var city in cities)
            {
                context.Cities.Add(city);
                await context.SaveChangesAsync();
            }


            var roles = new List<RoleEntity>
            {
                new RoleEntity
                {
                    Name = "Client"
                },
               new RoleEntity
               {
                    Name = "Seller"
               },
               new RoleEntity
               {
                   Name = "Doctor"
               }
            };

            foreach (var role in roles)
                await roleManager.CreateAsync(role);

            var admin = new UserEntity
            {
                Email = "admin@test.com",
                UserName = "admin@test.com",
                CityId = 1,
            };
            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Seller", "Client" , "Doctor" });

        }
    }
}
