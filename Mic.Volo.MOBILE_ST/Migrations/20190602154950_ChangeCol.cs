using Microsoft.EntityFrameworkCore.Migrations;

namespace Mic.Volo.MOBILE_ST.Migrations
{
    public partial class ChangeCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Phones_PhoneId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "CakeId",
                table: "ShoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "CakeName",
                table: "OrderDetails",
                newName: "PhoneName");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneId",
                table: "ShoppingCartItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Phones_PhoneId",
                table: "ShoppingCartItems",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Phones_PhoneId",
                table: "ShoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "PhoneName",
                table: "OrderDetails",
                newName: "CakeName");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneId",
                table: "ShoppingCartItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CakeId",
                table: "ShoppingCartItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Phones_PhoneId",
                table: "ShoppingCartItems",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
