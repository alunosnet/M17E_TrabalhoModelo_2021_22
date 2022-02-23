namespace M17E_TrabalhoModelo_2021_22.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class utilizadores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false),
                        perfil = c.Int(nullable: false),
                        estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
