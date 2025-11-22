namespace SmartTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskItems",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false),
                        Description = c.String(),
                        Status = c.String(nullable: false),
                        EstimatedTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AssignedToUserId = c.Int(nullable: false),
                        AssignedByUserId = c.Int(nullable: false),
                        Department = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ReviewStatus = c.String(),
                        ManagerComments = c.String(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.AppUsers", t => t.AssignedByUserId)
                .ForeignKey("dbo.AppUsers", t => t.AssignedToUserId)
                .Index(t => t.AssignedToUserId)
                .Index(t => t.AssignedByUserId);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        Role = c.String(),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskItems", "AssignedToUserId", "dbo.AppUsers");
            DropForeignKey("dbo.TaskItems", "AssignedByUserId", "dbo.AppUsers");
            DropIndex("dbo.TaskItems", new[] { "AssignedByUserId" });
            DropIndex("dbo.TaskItems", new[] { "AssignedToUserId" });
            DropTable("dbo.AppUsers");
            DropTable("dbo.TaskItems");
        }
    }
}
