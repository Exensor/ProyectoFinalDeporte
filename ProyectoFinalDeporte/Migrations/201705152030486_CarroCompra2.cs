namespace ProyectoFinalDeporte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarroCompra2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "ProductId");
            AddForeignKey("dbo.Carts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            DropColumn("dbo.Carts", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Name", c => c.String());
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropColumn("dbo.Carts", "ProductId");
        }
    }
}
