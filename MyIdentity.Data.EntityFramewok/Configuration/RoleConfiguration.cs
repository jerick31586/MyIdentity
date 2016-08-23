using MyIdentity.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MyIdentity.Data.EntityFramewok.Configuration
{
    internal class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        internal RoleConfiguration()
        {
            ToTable("Roles");

            HasKey(x => x.RoleID)
                .Property(x => x.RoleID)
                .HasColumnName("RoleID")
                //.HasColumnType("nvarchar")
                .HasMaxLength(128)                
                .IsRequired();

            Property(x => x.RoleName)
                .HasColumnName("RoleName")
                //.HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
