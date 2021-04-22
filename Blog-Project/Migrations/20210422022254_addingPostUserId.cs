using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog_Project.Migrations
{
    public partial class addingPostUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Posts",
                type: "NVARCHAR(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MainComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 4, 21, 22, 22, 53, 772, DateTimeKind.Local).AddTicks(7921));

            migrationBuilder.UpdateData(
                table: "MainComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 4, 21, 22, 22, 53, 776, DateTimeKind.Local).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "MainComments",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 4, 21, 22, 22, 53, 776, DateTimeKind.Local).AddTicks(3068));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 4, 21, 22, 22, 53, 776, DateTimeKind.Local).AddTicks(9068));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 4, 21, 22, 22, 53, 777, DateTimeKind.Local).AddTicks(765));

            migrationBuilder.UpdateData(
                table: "SubComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 4, 21, 22, 22, 53, 776, DateTimeKind.Local).AddTicks(4737));

            migrationBuilder.UpdateData(
                table: "SubComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 4, 21, 22, 22, 53, 776, DateTimeKind.Local).AddTicks(5103));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "MainComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 4, 17, 23, 56, 27, 264, DateTimeKind.Local).AddTicks(904));

            migrationBuilder.UpdateData(
                table: "MainComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 4, 17, 23, 56, 27, 267, DateTimeKind.Local).AddTicks(1595));

            migrationBuilder.UpdateData(
                table: "MainComments",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 4, 17, 23, 56, 27, 267, DateTimeKind.Local).AddTicks(1699));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 4, 17, 23, 56, 27, 267, DateTimeKind.Local).AddTicks(7014));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 4, 17, 23, 56, 27, 267, DateTimeKind.Local).AddTicks(8503));

            migrationBuilder.UpdateData(
                table: "SubComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 4, 17, 23, 56, 27, 267, DateTimeKind.Local).AddTicks(3169));

            migrationBuilder.UpdateData(
                table: "SubComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 4, 17, 23, 56, 27, 267, DateTimeKind.Local).AddTicks(3467));
        }
    }
}
