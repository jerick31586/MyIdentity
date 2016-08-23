namespace EFMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasMaxLenghtAnnotionsToAllFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false));
        }
    }
}
