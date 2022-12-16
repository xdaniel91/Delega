using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(202212160910, "Relation with address")]
public class PersonAddressMigration : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Alter.Table("person")
            .AddColumn("id_address")
            .AsInt64()
            .ForeignKey("fk_address_person", "address", "id");

    }
}
