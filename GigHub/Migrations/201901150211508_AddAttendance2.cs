namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Attendances", name: "AttendeeId_Id", newName: "AttendanceId_Id");
            RenameIndex(table: "dbo.Attendances", name: "IX_AttendeeId_Id", newName: "IX_AttendanceId_Id");
            DropPrimaryKey("dbo.Attendances");
            AddColumn("dbo.Attendances", "AttendeeId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendeeId" });
            DropColumn("dbo.Attendances", "AttendaceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendances", "AttendaceId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Attendances");
            DropColumn("dbo.Attendances", "AttendeeId");
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendaceId" });
            RenameIndex(table: "dbo.Attendances", name: "IX_AttendanceId_Id", newName: "IX_AttendeeId_Id");
            RenameColumn(table: "dbo.Attendances", name: "AttendanceId_Id", newName: "AttendeeId_Id");
        }
    }
}
