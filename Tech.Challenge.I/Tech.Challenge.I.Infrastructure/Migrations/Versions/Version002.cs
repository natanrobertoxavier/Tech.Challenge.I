using FluentMigrator;

namespace Tech.Challenge.I.Infrastructure.Migrations.Versions;

[Migration((long)NumberVersions.CreateDDDTable, "Create DDDRegions table")]
public class Version002 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        var tabela = VersionBase.InsertStandardColumns(Create.Table("DDDRegions"));

        tabela
            .WithColumn("DDD").AsInt32().NotNullable()
            .WithColumn("Region").AsString(30).NotNullable();

        Execute.Sql(@"ALTER TABLE DDDRegions
                          ADD CONSTRAINT CK_Region CHECK (Region IN ('Norte', 'Nordeste', 'CentroOeste', 'Sudeste', 'Sul'));");
    }
}
