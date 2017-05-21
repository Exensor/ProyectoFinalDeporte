namespace ProyectoFinalDeporte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Administrador : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        IdUser = c.String(),
                        IsAdmin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Admins");
        }
    }
}
