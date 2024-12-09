namespace TableDims.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mThird : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAccessDetails",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserName = c.String(),
                        LoginDate = c.DateTime(nullable: false),
                        LoginTime = c.DateTime(nullable: false),
                        ModIdAccessed = c.Int(nullable: false),
                        SubModIdAccessed = c.Int(nullable: false),
                        FormAccessed = c.String(maxLength: 150),
                        LogOutDate = c.DateTime(nullable: false),
                        LogOutTime = c.DateTime(nullable: false),
                        ModMaster_ModId = c.Int(),
                        SubModMaster_SubModId = c.Int(),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.ModMasters", t => t.ModMaster_ModId)
                .ForeignKey("dbo.RoleMasters", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.SubModMasters", t => t.SubModMaster_SubModId)
                .Index(t => t.RoleId)
                .Index(t => t.ModMaster_ModId)
                .Index(t => t.SubModMaster_SubModId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAccessDetails", "SubModMaster_SubModId", "dbo.SubModMasters");
            DropForeignKey("dbo.UserAccessDetails", "RoleId", "dbo.RoleMasters");
            DropForeignKey("dbo.UserAccessDetails", "ModMaster_ModId", "dbo.ModMasters");
            DropIndex("dbo.UserAccessDetails", new[] { "SubModMaster_SubModId" });
            DropIndex("dbo.UserAccessDetails", new[] { "ModMaster_ModId" });
            DropIndex("dbo.UserAccessDetails", new[] { "RoleId" });
            DropTable("dbo.UserAccessDetails");
        }
    }
}
