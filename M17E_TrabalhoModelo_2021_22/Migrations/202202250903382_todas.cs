namespace M17E_TrabalhoModelo_2021_22.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class todas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 80),
                        Morada = c.String(nullable: false, maxLength: 110),
                        CP = c.String(nullable: false, maxLength: 8),
                        Email = c.String(nullable: false),
                        Telefone = c.String(maxLength: 15),
                        DataNascimento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteID);
            
            CreateTable(
                "dbo.Estadias",
                c => new
                    {
                        EstadiaID = c.Int(nullable: false, identity: true),
                        data_entrada = c.DateTime(nullable: false),
                        data_saida = c.DateTime(nullable: false),
                        valor_pago = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClienteID = c.Int(nullable: false),
                        QuartoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstadiaID)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .ForeignKey("dbo.Quartos", t => t.QuartoID, cascadeDelete: true)
                .Index(t => t.ClienteID)
                .Index(t => t.QuartoID);
            
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
            DropForeignKey("dbo.Estadias", "QuartoID", "dbo.Quartos");
            DropForeignKey("dbo.Estadias", "ClienteID", "dbo.Clientes");
            DropIndex("dbo.Estadias", new[] { "QuartoID" });
            DropIndex("dbo.Estadias", new[] { "ClienteID" });
            DropTable("dbo.Users");
            DropTable("dbo.Quartos");
            DropTable("dbo.Estadias");
            DropTable("dbo.Clientes");
        }
    }
}
