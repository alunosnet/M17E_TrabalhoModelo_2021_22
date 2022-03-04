namespace M17E_TrabalhoModelo_2021_22.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useratualizado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false));
        }
    }
}
