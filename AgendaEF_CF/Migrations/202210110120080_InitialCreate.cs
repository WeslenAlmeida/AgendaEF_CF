namespace AgendaEF_CF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phones", "Person_Id", "dbo.People");
            DropIndex("dbo.Phones", new[] { "Person_Id" });
            RenameColumn(table: "dbo.Phones", name: "Person_Id", newName: "PersonId");
            AlterColumn("dbo.Phones", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Phones", "PersonId");
            AddForeignKey("dbo.Phones", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            DropColumn("dbo.Phones", "NamePerson");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Phones", "NamePerson", c => c.String());
            DropForeignKey("dbo.Phones", "PersonId", "dbo.People");
            DropIndex("dbo.Phones", new[] { "PersonId" });
            AlterColumn("dbo.Phones", "PersonId", c => c.Int());
            RenameColumn(table: "dbo.Phones", name: "PersonId", newName: "Person_Id");
            CreateIndex("dbo.Phones", "Person_Id");
            AddForeignKey("dbo.Phones", "Person_Id", "dbo.People", "Id");
        }
    }
}
