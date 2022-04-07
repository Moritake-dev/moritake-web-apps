using MGAuthentication.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MGAuthentication.Data.User
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Ignore(x => x.FullName);
            builder.OwnsOne(x => x.Address);
            builder.Property(x => x.DateOfBirth)
                .IsRequired(false);
            builder.Property(x => x.EmergencyContactNumber)
                .IsRequired(false);

            //~~~~~~~~~~~~~~~~~~~ handling enums ~~~~~~~~~~~~~~~~~~~~~~~~
            builder.Property(p => p.Gender)
                .HasConversion(
                    v => v.ToString(),
                    v => (Gender)Enum.Parse(typeof(Gender), v));

            builder.Property(p => p.RestType)
                .HasConversion(
                    v => v.ToString(),
                    v => (RestType)Enum.Parse(typeof(RestType), v));

            builder.Property(p => p.BloodType)
                .HasConversion(v => v.ToString(),
                    v => (BloodType)Enum.Parse(typeof(BloodType), v));

            // Handling Relationships here
            // cause you don't have a real relationship

            builder.HasOne(x => x.Department)
                .WithMany(v => v.ApplicationUser)
                .HasForeignKey(x => x.DepartmentId);

            builder.HasOne(x => x.JobPosition)
                .WithMany(v => v.ApplicationUser)
                .HasForeignKey(x => x.JobPositionId);
        }
    }
}