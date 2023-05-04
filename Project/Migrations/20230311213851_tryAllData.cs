using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class tryAllData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllResData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
                    Room_type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Room_floor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Room_number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Arrival_time = table.Column<DateTime>(type: "Date", nullable: false),
                    Leaving_time = table.Column<DateTime>(type: "Date", nullable: false),
                    Check_in = table.Column<bool>(type: "bit", nullable: false),
                    Supply_status = table.Column<bool>(type: "bit", nullable: false),
                    First_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Birth_day = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apt_suite = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Payment_type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Card_type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Card_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Card_exp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Card_cvc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Total_bill = table.Column<float>(type: "real", nullable: false),
                    Breakfast = table.Column<int>(type: "int", nullable: false),
                    Lunch = table.Column<int>(type: "int", nullable: false),
                    Dinner = table.Column<int>(type: "int", nullable: false),
                    Cleaning = table.Column<bool>(type: "bit", nullable: false),
                    Towel = table.Column<bool>(type: "bit", nullable: false),
                    S_surprise = table.Column<bool>(type: "bit", nullable: false),
                    Food_bill = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllResData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllResData");
        }
    }
}
