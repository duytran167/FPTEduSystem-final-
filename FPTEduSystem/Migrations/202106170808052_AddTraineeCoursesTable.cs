namespace FPTEduSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTraineeCoursesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        TraineeID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeID)
                .Index(t => t.CourseID)
                .Index(t => t.TraineeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeCourses", "TraineeID", "dbo.AspNetUsers");
            DropForeignKey("dbo.TraineeCourses", "CourseID", "dbo.Courses");
            DropIndex("dbo.TraineeCourses", new[] { "TraineeID" });
            DropIndex("dbo.TraineeCourses", new[] { "CourseID" });
            DropTable("dbo.TraineeCourses");
        }
    }
}
