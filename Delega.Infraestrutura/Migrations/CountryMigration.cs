using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(20221215102825, "Country table")]
public class CountryMigration : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Create.Table("country")
             .WithColumn("id").AsInt64().PrimaryKey().Identity()
             .WithColumn("name").AsString(60).NotNullable().Unique()
             .WithColumn("created_at").AsDateTime().NotNullable();
    }
}
