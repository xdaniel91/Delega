using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(20221215102723, "Address table")]
public class AddressMigration : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Create.Table("address")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("street").AsString().NotNullable()
            .WithColumn("zip_code").AsString().NotNullable().Unique()
            .WithColumn("district").AsString().NotNullable()
            .WithColumn("number").AsInt32().Nullable()
            .WithColumn("createdtime").AsDateTime().WithDefaultValue(RawSql.Insert("NOW()"))
            .WithColumn("additional_information").AsString().Nullable()
            .WithColumn("id_city").AsInt64().ForeignKey("fk_id_city_address", "city", "id");
    }
}
