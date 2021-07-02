namespace FPTEduSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainerDepartmentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainerDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrainerId = c.String(maxLength: 128),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId)
                .Index(t => t.TrainerId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerDepartments", "TrainerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TrainerDepartments", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.TrainerDepartments", new[] { "DepartmentId" });
            DropIndex("dbo.TrainerDepartments", new[] { "TrainerId" });
            DropTable("dbo.TrainerDepartments");
        }
    }
}
