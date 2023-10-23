using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Giift.GiiftCatalogModule.Data.Migrations
{
    public partial class ExtendCategoryItemRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "CategoryItemRelation",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "ExtendedCategoryItemRelationEntity");

            migrationBuilder.AddColumn<bool>(
                name: "IsItemRelationActive",
                table: "CategoryItemRelation",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CategoryItemRelation");

            migrationBuilder.DropColumn(
                name: "IsItemRelationActive",
                table: "CategoryItemRelation");
        }
    }
}
