namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mesa : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Mesas");
            AlterColumn("dbo.Mesas", "MesaId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Mesas", "MesaId");
            CreateIndex("dbo.Mesas", "MesaId");
            AddForeignKey("dbo.Mesas", "MesaId", "dbo.Restaurantes", "RestauranteId");
            DropColumn("dbo.Mesas", "RestauranteId");
            DropColumn("dbo.Restaurantes", "MesaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restaurantes", "MesaId", c => c.Int(nullable: false));
            AddColumn("dbo.Mesas", "RestauranteId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Mesas", "MesaId", "dbo.Restaurantes");
            DropIndex("dbo.Mesas", new[] { "MesaId" });
            DropPrimaryKey("dbo.Mesas");
            AlterColumn("dbo.Mesas", "MesaId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Mesas", "MesaId");
        }
    }
}
