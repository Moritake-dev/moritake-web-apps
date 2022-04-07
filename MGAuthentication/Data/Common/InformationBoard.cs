using MGAuthentication.Data.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MGAuthentication.Data.Common
{
    public class InformationBoard : AuditableEntity<int>
    {
        public string MessageTitle { get; set; }
        public string MessageDetail { get; set; }
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }

    public class InformationBoardConfiguration : IEntityTypeConfiguration<InformationBoard>
    {
        public void Configure(EntityTypeBuilder<InformationBoard> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(bc => bc.ApplicationUser)
                .WithMany(c => c.InformationBoard)
                .HasForeignKey(bc => bc.UserId);
        }
    }
}