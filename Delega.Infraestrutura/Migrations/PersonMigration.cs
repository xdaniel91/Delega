﻿using FluentMigrator;

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
        .WithColumn("id_address").AsInt64().ForeignKey("fk_address_person", "address", "id")
        .WithColumn("firstname").AsString().NotNullable()
        .WithColumn("lastname").AsString().NotNullable()
        .WithColumn("cpf").AsFixedLengthString(11).Unique().NotNullable()
        .WithColumn("age").AsInt32().NotNullable()
        .WithColumn("birth_date").AsDate().NotNullable()
        .WithColumn("created_at").AsDate().NotNullable();
    }
}
