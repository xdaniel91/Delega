using FluentMigrator;

namespace Delega.Api.Migrations;

[Migration(202209101647)]
public class AddPersonTable : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Create.Table("person")
        .WithColumn("id").AsInt64().PrimaryKey().Identity()
        .WithColumn("firstname").AsString().NotNullable()
        .WithColumn("lastname").AsString().NotNullable()
        .WithColumn("cpf").AsFixedLengthString(11).Unique().NotNullable()
        .WithColumn("birthdate").AsDate().NotNullable()
        .WithColumn("createdtime").AsDateTime().WithDefaultValue(SystemMethods.CurrentUTCDateTime)
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
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("personid").AsInt64().NotNullable().ForeignKey("person", "id")
            .WithColumn("oab").AsString().NotNullable()
            .WithColumn("createdtime").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime).NotNullable()
            .WithColumn("updatedtime").AsTime().Nullable();
    }
}
