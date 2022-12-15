using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(20221215102829, "City table")]
public class CityMigration : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Create.Table("city")
            .WithColumn("id").AsInt64().PrimaryKey()
            .WithColumn("id_state").AsInt64().ForeignKey("state", "id")
            .WithColumn("createdtime").AsDateTime().WithDefaultValue(RawSql.Insert("NOW()"))
            .WithColumn("name").AsString().NotNullable().Unique();
    }
}
