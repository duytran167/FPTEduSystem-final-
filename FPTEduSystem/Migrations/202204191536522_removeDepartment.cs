namespace FPTEduSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDepartment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrainerDepartments", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.TrainerDepartments", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.TrainerDepartments", new[] { "TrainerId" });
            DropIndex("dbo.TrainerDepartments", new[] { "DepartmentId" });
            DropTable("dbo.Departments");
            DropTable("dbo.TrainerDepartments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TrainerDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrainerId = c.String(maxLength: 128),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false),
                        Detail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TrainerDepartments", "DepartmentId");
            CreateIndex("dbo.TrainerDepartments", "TrainerId");
            AddForeignKey("dbo.TrainerDepartments", "TrainerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TrainerDepartments", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
