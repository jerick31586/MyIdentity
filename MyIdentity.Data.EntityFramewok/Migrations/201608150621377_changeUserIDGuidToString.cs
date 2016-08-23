namespace MyIdentity.Data.EntityFramewok.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserIDGuidToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRole", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserID", "dbo.Users");
            DropIndex("dbo.UserClaims", new[] { "UserID" });
            DropIndex("dbo.UserLogins", new[] { "UserID" });
            DropIndex("dbo.UserRole", new[] { "UserID" });
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.UserLogins");
            DropPrimaryKey("dbo.UserRole");
            AlterColumn("dbo.Users", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserClaims", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserLogins", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRole", "UserID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "UserID");
            AddPrimaryKey("dbo.UserLogins", new[] { "ProviderKey", "LoginProvider", "UserID" });
            AddPrimaryKey("dbo.UserRole", new[] { "UserID", "RoleID" });
            CreateIndex("dbo.UserClaims", "UserID");
            CreateIndex("dbo.UserLogins", "UserID");
            CreateIndex("dbo.UserRole", "UserID");
            AddForeignKey("dbo.UserRole", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.UserClaims", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.UserLogins", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLogins", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserRole", "UserID", "dbo.Users");
            DropIndex("dbo.UserRole", new[] { "UserID" });
            DropIndex("dbo.UserLogins", new[] { "UserID" });
            DropIndex("dbo.UserClaims", new[] { "UserID" });
            DropPrimaryKey("dbo.UserRole");
            DropPrimaryKey("dbo.UserLogins");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.UserRole", "UserID", c => c.Guid(nullable: false));
            AlterColumn("dbo.UserLogins", "UserID", c => c.Guid(nullable: false));
            AlterColumn("dbo.UserClaims", "UserID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Users", "UserID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.UserRole", new[] { "UserID", "RoleID" });
            AddPrimaryKey("dbo.UserLogins", new[] { "ProviderKey", "LoginProvider", "UserID" });
            AddPrimaryKey("dbo.Users", "UserID");
            CreateIndex("dbo.UserRole", "UserID");
            CreateIndex("dbo.UserLogins", "UserID");
            CreateIndex("dbo.UserClaims", "UserID");
            AddForeignKey("dbo.UserLogins", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.UserClaims", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.UserRole", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}
