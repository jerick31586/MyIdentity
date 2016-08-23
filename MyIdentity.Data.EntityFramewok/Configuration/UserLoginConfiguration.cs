using MyIdentity.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MyIdentity.Data.EntityFramewok.Configuration
{
    internal class UserLoginConfiguration : EntityTypeConfiguration<UserLogin>
    {
        internal UserLoginConfiguration()
        {
            ToTable("UserLogins");

            HasKey(x => new { x.ProviderKey, x.LoginProvider, x.UserID });

            Property(x => x.ProviderKey)
                .HasColumnName("ProviderKey")
                //.HasColumnType("nvarchar")
                .HasMaxLength(128)
                .IsRequired();

            Property(x => x.LoginProvider)
                .HasColumnName("LoginProvider")
                //.HasColumnType("nvarchar")
                .HasMaxLength(128)
                .IsRequired();

            Property(x => x.UserID)
                .HasColumnName("UserID")
                //.HasColumnType("nvarchar")
                .HasMaxLength(128)               
                .IsRequired();
        }
    }
}
