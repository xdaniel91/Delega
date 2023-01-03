using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(20221215174937, "Address table")]
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
            .WithColumn("zip_code").AsString(20).NotNullable().Unique()
            .WithColumn("number").AsInt32().Nullable()
            .WithColumn("additional_information").AsString().Nullable()
            .WithColumn("district").AsString().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("id_city").AsInt64().ForeignKey("fk_city_address", "city", "id");
    }
}
