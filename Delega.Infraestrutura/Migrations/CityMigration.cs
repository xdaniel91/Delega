using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(20221215102888, "City table")]
public class CityMigration : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Create.Table("city")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("id_state").AsInt64().ForeignKey("fk_state_city", "state", "id")
            .WithColumn("name").AsString(59).NotNullable().Unique();
    }
}
