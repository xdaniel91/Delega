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

[Migration(202209120936)]
public class AddLawyerTable : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Create.Table("lawyer")
            .WithColumn("id").AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn("personid").AsInt64().ForeignKey("person", "id").NotNullable()
            .WithColumn("oab").AsString().NotNullable()
            .WithColumn("createdtime").AsDateTime().WithDefaultValue(DateTime.UtcNow)
            .WithColumn("updatedtime").AsTime().Nullable();
    }
}
