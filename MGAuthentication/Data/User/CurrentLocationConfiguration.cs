using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MGAuthentication.Data.User
{
    public class CurrentLocationConfiguration : IEntityTypeConfiguration<CurrentLocation>
    {
        public void Configure(EntityTypeBuilder<CurrentLocation> builder)
        {
        }
    }
}