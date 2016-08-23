namespace MyIdentity.Data.EntityFramewok.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFirstNameAndLastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 4000));
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
