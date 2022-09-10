using FluentMigrator;

namespace Delega.Api.Migrations;

[Migration(202209101647)]
public class AddPersonTable : Migration
{
    public override void Down()
    {
        Delete.Column("person");
    }

    public override void Up()
    {
        Create.Table("person")
        .WithColumn("id").AsInt64().PrimaryKey().Identity().NotNullable()
        .WithColumn("firstname").AsString().NotNullable()
        .WithColumn("lastname").AsString().NotNullable()
        .WithColumn("cpf").AsFixedLengthString(11).Unique().NotNullable()
        .WithColumn("birthdate").AsDate().NotNullable()
        .WithColumn("createdtime").AsDateTime().WithDefaultValue(DateTime.UtcNow)
        .WithColumn("updatedtime").AsTime().Nullable();
    }
}
