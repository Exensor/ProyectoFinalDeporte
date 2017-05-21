namespace ProyectoFinalDeporte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarroCompra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CartID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Carts");
        }
    }
}
