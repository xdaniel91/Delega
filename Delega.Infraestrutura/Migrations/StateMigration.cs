using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(20221215171523, "State table")]
public class StateMigration : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Create.Table("state")
         .WithColumn("id").AsInt64().PrimaryKey().Identity()
         .WithColumn("id_country").AsInt64().ForeignKey("fk_country_state", "country", "id")
         .WithColumn("created_at").AsDateTime().NotNullable()
         .WithColumn("name").AsString().NotNullable().Unique()
         ;
    }
}
