namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MesaPlato2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Platoes", "RestauranteId");
            AddForeignKey("dbo.Platoes", "RestauranteId", "dbo.Restaurantes", "RestauranteId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Platoes", "RestauranteId", "dbo.Restaurantes");
            DropIndex("dbo.Platoes", new[] { "RestauranteId" });
        }
    }
}
