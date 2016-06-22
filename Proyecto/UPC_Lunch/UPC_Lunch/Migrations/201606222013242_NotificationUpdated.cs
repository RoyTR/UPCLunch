namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "PlatoId", "dbo.Platoes");
            DropIndex("dbo.Notifications", new[] { "PlatoId" });
            DropColumn("dbo.Notifications", "PlatoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "PlatoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "PlatoId");
            AddForeignKey("dbo.Notifications", "PlatoId", "dbo.Platoes", "PlatoId", cascadeDelete: true);
        }
    }
}
