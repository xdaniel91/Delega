using FluentMigrator;

namespace Delega.Infraestrutura.Migrations;

[Migration(20221214085123, "Add some persons on table")]
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
