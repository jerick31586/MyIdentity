namespace MyIdentity.Data.EntityFramewok.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoleIDDataType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRole", "RoleID", "dbo.Roles");
            DropIndex("dbo.UserRole", new[] { "RoleID" });
            DropPrimaryKey("dbo.Roles");
            DropPrimaryKey("dbo.UserRole");
            AlterColumn("dbo.Roles", "RoleID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRole", "RoleID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Roles", "RoleID");
            AddPrimaryKey("dbo.UserRole", new[] { "UserID", "RoleID" });
            CreateIndex("dbo.UserRole", "RoleID");
            AddForeignKey("dbo.UserRole", "RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleID", "dbo.Roles");
            DropIndex("dbo.UserRole", new[] { "RoleID" });
            DropPrimaryKey("dbo.UserRole");
            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.UserRole", "RoleID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Roles", "RoleID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.UserRole", new[] { "UserID", "RoleID" });
            AddPrimaryKey("dbo.Roles", "RoleID");
            CreateIndex("dbo.UserRole", "RoleID");
            AddForeignKey("dbo.UserRole", "RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
        }
    }
}
