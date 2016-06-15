namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restaurantes",
                c => new
                    {
                        RestauranteId = c.Int(nullable: false, identity: true),
                        RazonSocial = c.String(),
                        RUC = c.String(),
                        Direccion = c.String(),
                        Latitud = c.String(),
                        Longitud = c.String(),
                        AforoCompleto = c.Int(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.RestauranteId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Restaurantes");
        }
    }
}
