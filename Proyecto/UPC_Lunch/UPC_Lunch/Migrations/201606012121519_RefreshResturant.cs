namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefreshResturant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurantes", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurantes", "RazonSocial", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurantes", "RUC", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurantes", "Direccion", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurantes", "Latitud", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurantes", "Longitud", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurantes", "Estado", c => c.String(nullable: false));
            DropTable("dbo.UsuarioManages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsuarioManages",
                c => new
                    {
                        UsuarioManageId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioManageId);
            
            AlterColumn("dbo.Restaurantes", "Estado", c => c.String());
            AlterColumn("dbo.Restaurantes", "Longitud", c => c.String());
            AlterColumn("dbo.Restaurantes", "Latitud", c => c.String());
            AlterColumn("dbo.Restaurantes", "Direccion", c => c.String());
            AlterColumn("dbo.Restaurantes", "RUC", c => c.String());
            AlterColumn("dbo.Restaurantes", "RazonSocial", c => c.String());
            DropColumn("dbo.Restaurantes", "Email");
        }
    }
}
