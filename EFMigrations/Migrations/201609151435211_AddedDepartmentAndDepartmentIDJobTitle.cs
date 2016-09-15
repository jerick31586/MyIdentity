namespace EFMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDepartmentAndDepartmentIDJobTitle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            AddColumn("dbo.Users", "DepartmentID", c => c.Int());
            AddColumn("dbo.Users", "JobTitle", c => c.String(maxLength: 50));
            CreateIndex("dbo.Users", "DepartmentID");
            AddForeignKey("dbo.Users", "DepartmentID", "dbo.Departments", "DepartmentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "DepartmentID" });
            DropColumn("dbo.Users", "JobTitle");
            DropColumn("dbo.Users", "DepartmentID");
            DropTable("dbo.Departments");
        }
    }
}
