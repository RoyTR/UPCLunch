namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MesaPlato : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mesas",
                c => new
                    {
                        MesaId = c.Int(nullable: false, identity: true),
                        Disponible = c.Boolean(nullable: false),
                        RestauranteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MesaId);
            
            CreateTable(
                "dbo.Platoes",
                c => new
                    {
                        PlatoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Disponible = c.Boolean(nullable: false),
                        RestauranteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlatoId);
            
            AddColumn("dbo.Restaurantes", "MesaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurantes", "MesaId");
            DropTable("dbo.Platoes");
            DropTable("dbo.Mesas");
        }
    }
}
