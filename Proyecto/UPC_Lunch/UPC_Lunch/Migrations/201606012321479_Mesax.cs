namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mesax : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mesas", "MesaId", "dbo.Restaurantes");
            DropIndex("dbo.Mesas", new[] { "MesaId" });
            AddColumn("dbo.Restaurantes", "MesaDisponible", c => c.Boolean(nullable: false));
            DropTable("dbo.Mesas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Mesas",
                c => new
                    {
                        MesaId = c.Int(nullable: false),
                        Disponible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MesaId);
            
            DropColumn("dbo.Restaurantes", "MesaDisponible");
            CreateIndex("dbo.Mesas", "MesaId");
            AddForeignKey("dbo.Mesas", "MesaId", "dbo.Restaurantes", "RestauranteId");
        }
    }
}
