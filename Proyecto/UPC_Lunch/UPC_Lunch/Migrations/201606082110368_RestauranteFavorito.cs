namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestauranteFavorito : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestauranteFavoritoes",
                c => new
                    {
                        RestauranteFavoritoId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        RestauranteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RestauranteFavoritoId)
                .ForeignKey("dbo.Restaurantes", t => t.RestauranteId, cascadeDelete: true)
                .Index(t => t.RestauranteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestauranteFavoritoes", "RestauranteId", "dbo.Restaurantes");
            DropIndex("dbo.RestauranteFavoritoes", new[] { "RestauranteId" });
            DropTable("dbo.RestauranteFavoritoes");
        }
    }
}
