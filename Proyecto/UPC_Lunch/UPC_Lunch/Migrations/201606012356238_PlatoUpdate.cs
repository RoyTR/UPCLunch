namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlatoUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Platoes", "RestauranteId", "dbo.Restaurantes");
            DropIndex("dbo.Platoes", new[] { "RestauranteId" });
            RenameColumn(table: "dbo.Platoes", name: "RestauranteId", newName: "Restaurante_RestauranteId");
            AlterColumn("dbo.Platoes", "Restaurante_RestauranteId", c => c.Int());
            CreateIndex("dbo.Platoes", "Restaurante_RestauranteId");
            AddForeignKey("dbo.Platoes", "Restaurante_RestauranteId", "dbo.Restaurantes", "RestauranteId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Platoes", "Restaurante_RestauranteId", "dbo.Restaurantes");
            DropIndex("dbo.Platoes", new[] { "Restaurante_RestauranteId" });
            AlterColumn("dbo.Platoes", "Restaurante_RestauranteId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Platoes", name: "Restaurante_RestauranteId", newName: "RestauranteId");
            CreateIndex("dbo.Platoes", "RestauranteId");
            AddForeignKey("dbo.Platoes", "RestauranteId", "dbo.Restaurantes", "RestauranteId", cascadeDelete: true);
        }
    }
}
