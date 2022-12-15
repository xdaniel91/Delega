using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(20221215102823, "State table")]
public class StateMigration : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Create.Table("state")
         .WithColumn("id").AsInt64().PrimaryKey().Identity()
         .WithColumn("created_at").AsDateTime().NotNullable().WithDefaultValue("NOW()")
         .WithColumn("name").AsString().NotNullable().Unique()
         .WithColumn("id_country").AsInt64().ForeignKey("country", "id");
    }
}
