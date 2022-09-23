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
            .WithColumn("updatedtime").AsTime().Nullable()
            .WithColumn("name").AsString().NotNullable();
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
            .WithColumn("updatedtime").AsTime().Nullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("cpf").AsString().NotNullable();
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
            .WithColumn("updatedtime").AsDateTime().Nullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("cpf").AsString().NotNullable();
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
            .WithColumn("lawyerid").AsInt64().NotNullable().ForeignKey("lawyer", "id")
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

[Migration(20220923082623, "Add some people on person table.")]
public class FeedPersonTable : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        var sql = @"INSERT INTO person 
                    (firstname, lastname, cpf, birthdate, createdtime)
                VALUES 
                ('Vugath', 'Orgixu', '93562796080', '1999-05-05', now() ),
                ('Duermo', 'Arbeaim', '88070486040', '1999-05-05', now() ),
                ('Bolma', 'Veythal', '32332748075', '1999-05-05', now() ),
                ('Urcio', 'Begoli', '03302963025', '1999-05-05', now() ),
                ('Cikeybir', 'Tiur', '08250390024', '1999-05-05', now() ),
                ('Gaofogath', 'Reirius', '94300460043', '1999-05-05', now() ),
                ('Urpyekas', 'Valhoyhi', '28448726030', '1999-05-05', now() );";

        Execute.Sql(sql);
    }
}

