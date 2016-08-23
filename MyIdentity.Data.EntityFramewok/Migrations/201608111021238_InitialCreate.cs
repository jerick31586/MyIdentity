namespace MyIdentity.Data.EntityFramewok.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Guid(nullable: false),
                        RoleName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 4000),
                        Email = c.String(maxLength: 100),
                        Address = c.String(maxLength: 4000),
                        PhoneNumber = c.String(maxLength: 20, unicode: false),
                        DateOfBirth = c.DateTime(),
                        PasswordHash = c.String(nullable: false, maxLength: 4000),
                        SecurityStamp = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        ClaimID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        ClaimType = c.String(maxLength: 4000),
                        ClaimValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ClaimID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        UserID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProviderKey, t.LoginProvider, t.UserID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        RoleID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.RoleID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLogins", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.UserRole", "UserID", "dbo.Users");
            DropIndex("dbo.UserRole", new[] { "RoleID" });
            DropIndex("dbo.UserRole", new[] { "UserID" });
            DropIndex("dbo.UserLogins", new[] { "UserID" });
            DropIndex("dbo.UserClaims", new[] { "UserID" });
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
