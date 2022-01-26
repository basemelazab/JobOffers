namespace Offers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RulesViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RulesViewModels");
        }
    }
}
