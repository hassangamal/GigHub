namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenersTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into genres(Id,Name) Values(1,'Jazz')");
            Sql("Insert into genres(Id,Name) Values(2,'Blues')");
            Sql("Insert into genres(Id,Name) Values(3,'Rock')");
            Sql("Insert into genres(Id,Name) Values(4,'Country')");

        }

        public override void Down()
        {
            Sql("Delete from genres where Id IN(1,2,3,4)");
        }
        
    }
}
