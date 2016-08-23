namespace MyIdentity.Data.EntityFramewok.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmailConfirmedFieldToDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "EmailConfimed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "EmailConfimed");
        }
    }
}
