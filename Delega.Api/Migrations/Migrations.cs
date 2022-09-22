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

[Migration(20220921151633)]
public class AddAuthorTable : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Create.Table("author")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("personid").AsInt64().NotNullable().ForeignKey("person", "id")
            .WithColumn("depoiment").AsString().NotNullable()
            .WithColumn("createdtime").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime).NotNullable()
            .WithColumn("updatedtime").AsTime().Nullable();
    }
}


[Migration(20220921151520)]
public class AddAccusedTable : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Create.Table("accused")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("personid").AsInt64().NotNullable().ForeignKey("person", "id")
            .WithColumn("innocent").AsBoolean()
            .WithColumn("createdtime").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime).NotNullable()
            .WithColumn("updatedtime").AsTime().Nullable();
    }
}

[Migration(20220921161320)]
public class AddJudicialProcessTable : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Create.Table("judicialprocess")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("accusedid").AsInt64().NotNullable().ForeignKey("accused", "id")
            .WithColumn("authorid").AsInt64().NotNullable().ForeignKey("author", "id")
            .WithColumn("requestedvalue").AsDecimal().NotNullable()
            .WithColumn("value").AsDecimal().Nullable()
            .WithColumn("reason").AsString().NotNullable()
            .WithColumn("verdict").AsString().Nullable()
            .WithColumn("status").AsInt16().NotNullable()
            .WithColumn("datehourcreated").AsDateTime().NotNullable()
            .WithColumn("datehourinprogress").AsDateTime().Nullable()
            .WithColumn("datehourfinished").AsDateTime().Nullable();
    }
}