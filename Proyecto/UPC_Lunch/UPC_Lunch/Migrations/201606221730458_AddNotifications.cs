namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Description = c.String(),
                        Seen = c.Boolean(nullable: false),
                        RestauranteId = c.Int(nullable: false),
                        PlatoId = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.Platoes", t => t.PlatoId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurantes", t => t.RestauranteId, cascadeDelete: true)
                .Index(t => t.RestauranteId)
                .Index(t => t.PlatoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "RestauranteId", "dbo.Restaurantes");
            DropForeignKey("dbo.Notifications", "PlatoId", "dbo.Platoes");
            DropIndex("dbo.Notifications", new[] { "PlatoId" });
            DropIndex("dbo.Notifications", new[] { "RestauranteId" });
            DropTable("dbo.Notifications");
        }
    }
}
