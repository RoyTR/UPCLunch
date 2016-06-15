namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserManage : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsuarioManages");
        }
    }
}
