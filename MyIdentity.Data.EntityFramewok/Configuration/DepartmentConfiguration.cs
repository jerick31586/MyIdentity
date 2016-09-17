using System.Data.Entity.ModelConfiguration;
using MyIdentity.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIdentity.Data.EntityFramewok.Configuration
{
    internal class DepartmentConfiguration : EntityTypeConfiguration<Department> 
    {
        internal DepartmentConfiguration()
        {
            ToTable("Departments");

            HasKey(x => x.DepartmentID)
                .Property(x => x.DepartmentID)
                .HasColumnName("DepartmentID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.DepartmentName)
                .HasColumnName("DepartmentName")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
