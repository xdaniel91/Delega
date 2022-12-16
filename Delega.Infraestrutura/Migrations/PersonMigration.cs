using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(20221214085023, "Add person table")]
public class PersonMigration : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Create.Table("person")
        .WithColumn("id").AsInt64().PrimaryKey().Identity()
        .WithColumn("first_name").AsString().NotNullable()
        .WithColumn("last_name").AsString().NotNullable()
        .WithColumn("cpf").AsFixedLengthString(11).Unique().NotNullable()
        .WithColumn("age").AsInt32().NotNullable()
        .WithColumn("birth_date").AsDate().NotNullable()
        .WithColumn("created_at").AsDateTime().NotNullable();
    }
}
