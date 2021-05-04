using AppCore.Entity.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBS.DAL.Config
{
    public class AppUserClaimConfiguration : IEntityTypeConfiguration<AppUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppUserClaim> builder)
        {
            builder.HasKey(uc => uc.Id);
            builder.Property(uc => uc.Id).ValueGeneratedOnAdd();
            builder.Property(uc => uc.AppUserId).IsRequired();
            builder.Property(uc => uc.AppClaimId).IsRequired();

            builder.HasOne(q => q.AppUserFK).WithMany(q => q.AppUserClaims).HasForeignKey(q => q.AppUserId);
            builder.HasOne(q => q.AppClaimFK).WithMany(q => q.AppUserClaims).HasForeignKey(q => q.AppClaimId);
        }
    }
}