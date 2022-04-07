using MGAuthentication.Data.Common;
using MGAuthentication.Data.User;
using MGAuthentication.Services.CommonServices.CurrentUserServices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MGAuthentication.Data
{
    // REFER TO // KeyApiAuthorizationDbContext is from https://stackoverflow.com/questions/59997201/addapiauthorization-no-implicit-reference-conversion-asp-core-3-0
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity<int>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.UserId;
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;

                    // TODO: returns null reference
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.UpdatedBy = _currentUserService.UserId;
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        #region Required

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This loads all the configuration of the models
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #endregion Required

        public DbSet<Department> Departments { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<CurrentLocation> CurrentLocations { get; set; }
        public DbSet<UserCurrentLocation> UserCurrentLocation { get; set; }
        public DbSet<InformationBoard> InformationBoard { get; set; }
    }
}