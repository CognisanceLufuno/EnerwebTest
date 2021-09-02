namespace Lufuno.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileUploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.Guid(nullable: false),
                        FileName = c.String(nullable: false),
                        LoadDate = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false),
                        base64 = c.String(nullable: false),
                        NumberOfRecords = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.Guid(nullable: false),
                        FileId = c.Int(nullable: false),
                        OperatingDate = c.DateTime(nullable: false),
                        ServicePoint = c.String(),
                        HourNumber = c.Int(nullable: false),
                        UserId = c.String(),
                        ImportEnergy = c.Double(nullable: false),
                        ExportEnergy = c.Double(nullable: false),
                        ImportLeadingReactive = c.Double(nullable: false),
                        ExportLeadingReactive = c.Double(nullable: false),
                        ImportLaggingReactive = c.Double(nullable: false),
                        ExportLaggingReactive = c.Double(nullable: false),
                        IsOfficial = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        Upload_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileUploads", t => t.Upload_Id)
                .Index(t => t.Upload_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Records", "Upload_Id", "dbo.FileUploads");
            DropIndex("dbo.Records", new[] { "Upload_Id" });
            DropTable("dbo.Records");
            DropTable("dbo.FileUploads");
        }
    }
}
