namespace Offers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSearchTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "JobContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "JobContent");
        }
    }
}
