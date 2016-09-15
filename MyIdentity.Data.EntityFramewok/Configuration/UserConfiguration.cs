using System.Data.Entity.ModelConfiguration;
using MyIdentity.Domain.Entities;

namespace MyIdentity.Data.EntityFramewok.Configuration
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        internal UserConfiguration()
        {
            ToTable("Users");

            HasKey(x => x.UserID)
                .Property(x => x.UserID)
                .HasColumnName("UserID")
                //.HasColumnType("nvarchar")
                .HasMaxLength(128)
                .IsRequired();

            Property(x => x.UserName)
                .HasColumnName("UserName")
                //.HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsRequired();

            Property(x => x.FirstName)
                .HasColumnName("FirstName")
                //.HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(100)
                //.HasColumnType("nvarchar")
                .IsRequired();

            Property(x => x.PasswordHash)
                .HasColumnName("PasswordHash")
                //.HasColumnType("nvarchar")
                .IsMaxLength()                
                .IsRequired();

            Property(x => x.SecurityStamp)
                .HasColumnName("SecurityStamp")
                //.HasColumnType("nvarchar")
                .IsMaxLength()
                .IsOptional();

            Property(x => x.Email)
                .HasColumnName("Email")
                //.HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsOptional();

            Property(x => x.Address)
                .HasColumnName("Address")
                //.HasColumnType("nvarchar")
                .IsMaxLength()
                .IsOptional();

            Property(x => x.PhoneNumber)
                .HasColumnName("PhoneNumber")
                //.HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsOptional();

            Property(x => x.DateOfBirth)
                .HasColumnName("DateOfBirth")
                //.HasColumnType("datetime")                
                .IsOptional();

            Property(x => x.EmailConfirmed)
                .HasColumnName("EmailConfimed");
            //.HasColumnType("bit");

            Property(x => x.DepartmentID)
                .HasColumnName("DepartmentID")
                .IsOptional();

            Property(x => x.JobTitle)
                .HasColumnName("JobTitle")
                .IsOptional()
                .HasMaxLength(50);   
            //many to many relationship
            HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .Map(x =>
                {
                    x.ToTable("UserRole");
                    x.MapLeftKey("UserID");
                    x.MapRightKey("RoleID");
                });
            
            //one to many relationship
            HasMany(x => x.UserClaims)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserID);

            HasMany(x => x.UserLogins)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserID);
        }   
    }
}
