using MyIdentity.Domain.Entities;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyIdentity.Data.EntityFramewok.Configuration
{
    internal class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
    {
        internal UserClaimConfiguration()
        {
            ToTable("UserClaims");

            HasKey(x => x.ClaimID)
                .Property(x => x.ClaimID)
                .HasColumnName("ClaimID")
                //.HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.ClaimType)
                .HasColumnName("ClaimType")
                //.HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsMaxLength()
                .IsOptional();

            Property(x => x.ClaimValue)
                .HasColumnName("ClaimValue")
                //.HasColumnType("nvarchar")
                .IsMaxLength()
                .IsOptional();

            Property(x=>x.UserID)
                .HasColumnName("UserID")
                //.HasColumnType("nvarchar")
                .HasMaxLength(128)                
                .IsRequired();
        }
    }
}
