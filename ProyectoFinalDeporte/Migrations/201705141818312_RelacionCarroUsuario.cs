namespace ProyectoFinalDeporte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionCarroUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Carts", "UserId");
            AddForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropColumn("dbo.Carts", "UserId");
        }
    }
}
