using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MGAuthentication.Data.User
{
    public class UserCurrentLocationConfiguration : IEntityTypeConfiguration<UserCurrentLocation>
    {
        public void Configure(EntityTypeBuilder<UserCurrentLocation> builder)
        {
            builder.Property(x => x.ApprovedBy)
                .IsRequired(false);

            builder.HasKey(bc => new { bc.UserId, bc.CurrentLocationId });

            builder.HasOne(bc => bc.ApplicationUser)
                .WithMany(c => c.UserCurrentLocations)
                .HasForeignKey(bc => bc.UserId);

            builder.HasOne(bc => bc.CurrentLocation)
                .WithMany(c => c.UserCurrentLocations)
                .HasForeignKey(bc => bc.CurrentLocationId);
        }
    }
}