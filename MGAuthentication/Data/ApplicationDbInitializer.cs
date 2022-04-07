using MGAuthentication.Data.Common;
using MGAuthentication.Data.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace MGAuthentication.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedTables(context);

            SeedRoles(roleManager);

            SeedUser(context, userManager);
        }

        private static void SeedUser(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                ApplicationUser adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@email.com",
                    FirstName = "admin",
                    LastName = "seed",
                    Gender = Enums.Gender.Male,
                    BloodType = Enums.BloodType.APositive,
                    RestType = Enums.RestType.RestTypeOne,
                    DateOfBirth = DateTime.Parse("1992-06-04"),
                    EmergencyContactNumber = "+8108083999095",
                    IsActiveEmployee = true,
                    IsAdmin = true,
                    IsEmployeeProfile = false,
                    EmployeeOrder = 1,
                    DepartmentId = context.Departments.Where(x => x.Name == "SYSTEM").Select(x => x.Id).FirstOrDefault(),
                    JobPositionId = context.JobPositions.Where(x => x.Name == "SYSTEM").Select(x => x.Id).FirstOrDefault()
                };
                adminUser.Address = new Address(postNo: "8820052", detail: "41-1, Hagimachi, Nisshin Palace Stage 603");

                IdentityResult result = userManager.CreateAsync(adminUser, "adminPassword").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRolesAsync(adminUser, new[] { "Admin", "Manager" }).Wait();
                }
            }
        }

        private static void SeedTables(ApplicationDbContext context)
        {
            context.Database.Migrate();
            if (context.Departments.Where(x => x.Name == "SYSTEM").Select(x => x.Name).FirstOrDefault() != "SYSTEM")
            {
                context.Departments.Add(new Department { Name = "SYSTEM", CreatedDate = DateTime.UtcNow });
            }
            if (context.JobPositions.Where(x => x.Name == "SYSTEM").Select(x => x.Name).FirstOrDefault() != "SYSTEM")
            {
                context.JobPositions.Add(new JobPosition { Name = "SYSTEM", CreatedDate = DateTime.UtcNow });
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                IdentityRole adminRole = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                };

                // creating admin role
                roleManager.CreateAsync(adminRole).Wait();
                roleManager.AddClaimAsync(adminRole, new Claim("Permission", "can.create")).Wait();
                roleManager.AddClaimAsync(adminRole, new Claim("Permission", "can.read")).Wait();
                roleManager.AddClaimAsync(adminRole, new Claim("Permission", "can.update")).Wait();
                roleManager.AddClaimAsync(adminRole, new Claim("Permission", "can.delete")).Wait();
            }
            if (roleManager.FindByNameAsync("Manager").Result == null)
            {
                IdentityRole managerRole = new IdentityRole
                {
                    Name = "Manager",
                    NormalizedName = "Manager".ToUpper()
                };

                // creating manager role
                roleManager.CreateAsync(managerRole).Wait();
                roleManager.AddClaimAsync(managerRole, new Claim("Permission", "can.create")).Wait();
                roleManager.AddClaimAsync(managerRole, new Claim("Permission", "can.read")).Wait();
                roleManager.AddClaimAsync(managerRole, new Claim("Permission", "can.update")).Wait();
            }

            if (roleManager.FindByNameAsync("User").Result == null)
            {
                IdentityRole userRole = new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                };

                // creating user role
                roleManager.CreateAsync(userRole).Wait();
                roleManager.AddClaimAsync(userRole, new Claim("Permission", "can.create")).Wait();
                roleManager.AddClaimAsync(userRole, new Claim("Permission", "can.read")).Wait();
                roleManager.AddClaimAsync(userRole, new Claim("Permission", "can.update")).Wait();
            }
        }
    }
}