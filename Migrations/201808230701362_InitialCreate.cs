namespace OptimedCorporation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Menu = c.String(nullable: false),
                        BannerImage = c.String(),
                        Status = c.Boolean(nullable: false),
                        Ipaddress = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Careers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Mobile = c.String(nullable: false),
                        Dob = c.DateTime(nullable: false),
                        Experience = c.Int(nullable: false),
                        Qualification = c.String(),
                        SkillSet = c.String(),
                        Message = c.String(),
                        Resume = c.String(),
                        Ipaddress = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        StateId = c.Int(nullable: false),
                        CityName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.EventImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        Images = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        Location = c.String(),
                        Description = c.String(),
                        Thumbnail = c.String(),
                        Status = c.Boolean(nullable: false),
                        Ipaddress = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Pin = c.String(),
                        Message = c.String(),
                        Type = c.String(),
                        Ipaddress = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Heading = c.String(nullable: false),
                        NewsDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Thumbnail = c.String(),
                        FilePath = c.String(),
                        Status = c.Boolean(nullable: false),
                        Ipaddress = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StoreLocators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShopName = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        Pin = c.String(),
                        State = c.String(),
                        Longitude = c.String(),
                        Latitude = c.String(),
                        WorkingDays = c.String(),
                        WorkingTime = c.String(),
                        Status = c.Boolean(nullable: false),
                        Ipaddress = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventImages", "EventId", "dbo.Events");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropIndex("dbo.EventImages", new[] { "EventId" });
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropTable("dbo.StoreLocators");
            DropTable("dbo.News");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Events");
            DropTable("dbo.EventImages");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Careers");
            DropTable("dbo.Banners");
        }
    }
}
