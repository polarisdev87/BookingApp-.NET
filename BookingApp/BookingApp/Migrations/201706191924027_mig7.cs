namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accommodations",
                c => new
                    {
                        AccommodationId = c.Int(nullable: false, identity: true),
                        address = c.String(nullable: false, maxLength: 150),
                        approved = c.Boolean(nullable: false),
                        averageGrade = c.Single(nullable: false),
                        description = c.String(nullable: false, maxLength: 1000),
                        imageURL = c.String(nullable: false),
                        latitude = c.Double(nullable: false),
                        longitude = c.Double(nullable: false),
                        name = c.String(nullable: false, maxLength: 50),
                        PlaceId = c.Int(nullable: false),
                        AccommodationTypeId = c.Int(nullable: false),
                        AppUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccommodationId)
                .ForeignKey("dbo.AccommodationTypes", t => t.AccommodationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId)
                .Index(t => t.AccommodationTypeId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.AccommodationTypes",
                c => new
                    {
                        AccommodationTypeId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AccommodationTypeId)
                .Index(t => t.name, unique: true);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 25),
                        RoomReservations_RoomReservationsId = c.Int(),
                        Comment_CommentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomReservations", t => t.RoomReservations_RoomReservationsId)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.RoomReservations_RoomReservationsId)
                .Index(t => t.Comment_CommentId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        bedCount = c.Int(nullable: false),
                        description = c.String(nullable: false, maxLength: 500),
                        pricePerNight = c.Int(nullable: false),
                        roomNumber = c.Int(nullable: false),
                        AccommodationId = c.Int(nullable: false),
                        RoomReservations_RoomReservationsId = c.Int(),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Accommodations", t => t.AccommodationId, cascadeDelete: true)
                .ForeignKey("dbo.RoomReservations", t => t.RoomReservations_RoomReservationsId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AccommodationId)
                .Index(t => t.RoomReservations_RoomReservationsId)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.RoomReservations",
                c => new
                    {
                        RoomReservationsId = c.Int(nullable: false, identity: true),
                        endDate = c.String(nullable: false),
                        startDate = c.String(nullable: false),
                        timestamp = c.DateTime(nullable: false),
                        RoomId = c.Int(nullable: false),
                        AppUserId = c.Int(nullable: false),
                        Room_RoomId = c.Int(),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.RoomReservationsId)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: false)
                .ForeignKey("dbo.Rooms", t => t.Room_RoomId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.RoomId)
                .Index(t => t.AppUserId)
                .Index(t => t.Room_RoomId)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        grade = c.Int(nullable: false),
                        text = c.String(nullable: false, maxLength: 500),
                        AppUserId = c.Int(nullable: false),
                        AccommodationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Accommodations", t => t.AccommodationId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: false)
                .Index(t => t.AppUserId)
                .Index(t => t.AccommodationId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        PlaceId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 70),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlaceId)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.name, unique: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegionId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.name, unique: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 6),
                        name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CountryId)
                .Index(t => t.name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accommodations", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AppUsers", "Comment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "AccommodationId", "dbo.Accommodations");
            DropForeignKey("dbo.Accommodations", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.RoomReservations", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Rooms", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.RoomReservations", "Room_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomReservations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.AppUsers", "RoomReservations_RoomReservationsId", "dbo.RoomReservations");
            DropForeignKey("dbo.Rooms", "RoomReservations_RoomReservationsId", "dbo.RoomReservations");
            DropForeignKey("dbo.RoomReservations", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.Rooms", "AccommodationId", "dbo.Accommodations");
            DropForeignKey("dbo.Accommodations", "AccommodationTypeId", "dbo.AccommodationTypes");
            DropIndex("dbo.Countries", new[] { "name" });
            DropIndex("dbo.Regions", new[] { "CountryId" });
            DropIndex("dbo.Regions", new[] { "name" });
            DropIndex("dbo.Places", new[] { "RegionId" });
            DropIndex("dbo.Places", new[] { "name" });
            DropIndex("dbo.Comments", new[] { "AccommodationId" });
            DropIndex("dbo.Comments", new[] { "AppUserId" });
            DropIndex("dbo.RoomReservations", new[] { "AppUser_Id" });
            DropIndex("dbo.RoomReservations", new[] { "Room_RoomId" });
            DropIndex("dbo.RoomReservations", new[] { "AppUserId" });
            DropIndex("dbo.RoomReservations", new[] { "RoomId" });
            DropIndex("dbo.Rooms", new[] { "AppUser_Id" });
            DropIndex("dbo.Rooms", new[] { "RoomReservations_RoomReservationsId" });
            DropIndex("dbo.Rooms", new[] { "AccommodationId" });
            DropIndex("dbo.AppUsers", new[] { "Comment_CommentId" });
            DropIndex("dbo.AppUsers", new[] { "RoomReservations_RoomReservationsId" });
            DropIndex("dbo.AppUsers", new[] { "UserName" });
            DropIndex("dbo.AccommodationTypes", new[] { "name" });
            DropIndex("dbo.Accommodations", new[] { "AppUserId" });
            DropIndex("dbo.Accommodations", new[] { "AccommodationTypeId" });
            DropIndex("dbo.Accommodations", new[] { "PlaceId" });
            DropTable("dbo.Countries");
            DropTable("dbo.Regions");
            DropTable("dbo.Places");
            DropTable("dbo.Comments");
            DropTable("dbo.RoomReservations");
            DropTable("dbo.Rooms");
            DropTable("dbo.AppUsers");
            DropTable("dbo.AccommodationTypes");
            DropTable("dbo.Accommodations");
        }
    }
}
