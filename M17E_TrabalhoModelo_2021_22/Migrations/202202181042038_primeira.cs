namespace M17E_TrabalhoModelo_2021_22.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class primeira : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quartos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Piso = c.Int(nullable: false),
                        Lotacao = c.Int(nullable: false),
                        Custo_dia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Casa_banho = c.Boolean(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        Tipo_Quarto = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Quartos");
        }
    }
}
