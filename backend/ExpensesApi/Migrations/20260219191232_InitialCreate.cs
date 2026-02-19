using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpensesApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Keyword = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryRules_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountSource = table.Column<string>(type: "TEXT", nullable: false),
                    ImportBatchId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RawLine = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDefault", "Name" },
                values: new object[,]
                {
                    { 100, true, "Dagligvare" },
                    { 200, true, "Mat ute" },
                    { 300, true, "Hjem" },
                    { 400, true, "Underholdning" },
                    { 500, true, "Gaming" },
                    { 600, true, "Abonnement" },
                    { 700, true, "Netthandel" },
                    { 800, true, "Helse" },
                    { 900, true, "Kosmetikk" },
                    { 1000, true, "Kreditt" },
                    { 1100, true, "Transport" },
                    { 1200, true, "Bil" },
                    { 1300, true, "Bolig" },
                    { 1400, true, "Boutgifter" },
                    { 1500, true, "Forsikring" },
                    { 1600, true, "Overføring" },
                    { 1700, true, "Inntekt" },
                    { 1800, true, "Annet" }
                });

            migrationBuilder.InsertData(
                table: "CategoryRules",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Keyword", "Source" },
                values: new object[,]
                {
                    { 101, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "EXTRA", "seed" },
                    { 102, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "KIWI", "seed" },
                    { 103, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "REMA", "seed" },
                    { 104, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "COOP", "seed" },
                    { 105, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bunnpris", "seed" },
                    { 106, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Spar", "seed" },
                    { 107, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Joker", "seed" },
                    { 108, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Matkroken", "seed" },
                    { 109, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cc Mat", "seed" },
                    { 110, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mix", "seed" },
                    { 111, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vinmonopolet", "seed" },
                    { 112, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Obs", "seed" },
                    { 201, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Burger King", "seed" },
                    { 202, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mdc", "seed" },
                    { 203, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mcdonalds", "seed" },
                    { 204, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "BK", "seed" },
                    { 205, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Little Eat", "seed" },
                    { 206, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fly Chicken", "seed" },
                    { 207, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Expressenpizza", "seed" },
                    { 208, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pizzabakeren", "seed" },
                    { 209, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Raufoss Pizza", "seed" },
                    { 210, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dos Amigos", "seed" },
                    { 211, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sander Asia Mat", "seed" },
                    { 212, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gelatiamo", "seed" },
                    { 213, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Thai Bahn", "seed" },
                    { 214, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Wolt", "seed" },
                    { 215, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "foodora", "seed" },
                    { 216, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cafe Amsterdam", "seed" },
                    { 217, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hygge Gjøvik", "seed" },
                    { 219, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Javel Oslo", "seed" },
                    { 220, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Totensnacks", "seed" },
                    { 221, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "King Kebap", "seed" },
                    { 222, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Selecta", "seed" },
                    { 223, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Øya Maritim", "seed" },
                    { 224, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Max Burgers", "seed" },
                    { 225, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dominos Pizza", "seed" },
                    { 226, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Nebbenes Kro", "seed" },
                    { 227, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Shell", "seed" },
                    { 228, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "7-eleven", "seed" },
                    { 229, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Esso", "seed" },
                    { 230, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ST1", "seed" },
                    { 301, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "IKEA", "seed" },
                    { 302, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Søstrene Grene", "seed" },
                    { 303, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Biltema", "seed" },
                    { 304, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rusta", "seed" },
                    { 305, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Jula", "seed" },
                    { 306, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Clas Ohl", "seed" },
                    { 307, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Plantasjen", "seed" },
                    { 308, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kid", "seed" },
                    { 309, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Europris", "seed" },
                    { 310, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Normal", "seed" },
                    { 311, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Buddy", "seed" },
                    { 312, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Musti", "seed" },
                    { 313, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Jysk", "seed" },
                    { 314, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Nille", "seed" },
                    { 315, 300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sport 1", "seed" },
                    { 316, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kitch", "seed" },
                    { 401, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gjøvik Kino", "seed" },
                    { 402, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Norsk Tipping", "seed" },
                    { 403, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Totenbadet", "seed" },
                    { 404, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fjellhallen", "seed" },
                    { 405, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Geekevents", "seed" },
                    { 406, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sveve", "seed" },
                    { 407, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fun Padel", "seed" },
                    { 408, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Padel-senter", "seed" },
                    { 409, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mjøsstranda", "seed" },
                    { 410, 400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tikkio", "seed" },
                    { 501, 500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Steam", "seed" },
                    { 502, 500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "discord", "seed" },
                    { 503, 500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "riotgamesl", "seed" },
                    { 504, 500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "jagex", "seed" },
                    { 505, 500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "eneba", "seed" },
                    { 601, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "APPLE.COM/BILL", "seed" },
                    { 602, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tidalmusic", "seed" },
                    { 603, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Telenor", "seed" },
                    { 604, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Google One", "seed" },
                    { 605, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "splice", "seed" },
                    { 606, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Claude.ai", "seed" },
                    { 607, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Anthropic", "seed" },
                    { 608, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "OpenAI", "seed" },
                    { 609, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Scrimba", "seed" },
                    { 610, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Real Debrid", "seed" },
                    { 611, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Suno", "seed" },
                    { 612, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "elitehosting", "seed" },
                    { 613, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "agderposten", "seed" },
                    { 614, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Lyse Tele", "seed" },
                    { 615, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lennardigi", "seed" },
                    { 616, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "native Ins", "seed" },
                    { 617, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Netflix", "seed" },
                    { 618, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Disney Plus", "seed" },
                    { 619, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "storytel", "seed" },
                    { 620, 600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mova AS", "seed" },
                    { 701, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "CDON", "seed" },
                    { 702, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Maxgaming", "seed" },
                    { 703, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Junkyard", "seed" },
                    { 704, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "proshop", "seed" },
                    { 705, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Helthjem", "seed" },
                    { 706, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Thomann", "seed" },
                    { 707, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Power", "seed" },
                    { 708, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Xxl", "seed" },
                    { 709, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Norli", "seed" },
                    { 710, 700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "NOX PADEL", "seed" },
                    { 801, 800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Spesialistsenter", "seed" },
                    { 802, 800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Apotek", "seed" },
                    { 803, 800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Legevakt", "seed" },
                    { 804, 800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Helfo", "seed" },
                    { 805, 800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rtg.polikl", "seed" },
                    { 806, 800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vitus", "seed" },
                    { 807, 800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Legesenter", "seed" },
                    { 808, 800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Oslo Storbylege", "seed" },
                    { 809, 800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Farmasiet", "seed" },
                    { 901, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cutters", "seed" },
                    { 902, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Blivakker", "seed" },
                    { 903, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Nailster", "seed" },
                    { 904, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nordicnails", "seed" },
                    { 905, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "NORDICFEEL", "seed" },
                    { 906, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "KICKS", "seed" },
                    { 907, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rituals", "seed" },
                    { 908, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Feel Raufoss", "seed" },
                    { 909, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Zalando", "seed" },
                    { 910, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bubbleroom", "seed" },
                    { 911, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "CHANGE Lingerie", "seed" },
                    { 912, 900, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "LYKO", "seed" },
                    { 1001, 1000, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Klarna", "seed" },
                    { 1002, 1000, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Qliro", "seed" },
                    { 1003, 1000, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Credicare", "seed" },
                    { 1004, 1000, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tfbank", "seed" },
                    { 1005, 1000, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Riverty", "seed" },
                    { 1006, 1000, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ARVATO", "seed" },
                    { 1101, 1100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ruter", "seed" },
                    { 1102, 1100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Entur", "seed" },
                    { 1103, 1100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Crown Seaways", "seed" },
                    { 1104, 1100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "VY App", "seed" },
                    { 1105, 1100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Asfinag", "seed" },
                    { 1201, 1200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Uno-x", "seed" },
                    { 1202, 1200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tesla Inc", "seed" },
                    { 1203, 1200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "IONITY", "seed" },
                    { 1204, 1200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "KOPLE", "seed" },
                    { 1205, 1200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Recharge", "seed" },
                    { 1206, 1200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Easypark", "seed" },
                    { 1207, 1200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Apcoa", "seed" },
                    { 1208, 1200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "NORDEA FINANS", "seed" },
                    { 1301, 1300, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Nedbetaling", "seed" },
                    { 1401, 1400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fortum", "seed" },
                    { 1402, 1400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vestre Toten Kommune", "seed" },
                    { 1403, 1400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ringsaker Kommune", "seed" },
                    { 1404, 1400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Globalconnect", "seed" },
                    { 1405, 1400, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "VERISURE", "seed" },
                    { 1501, 1500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fremtind Forsikring", "seed" },
                    { 1502, 1500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "GJENSIDIGE", "seed" },
                    { 1503, 1500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "AGRIA", "seed" },
                    { 1601, 1600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Overføring Innland", "seed" },
                    { 1602, 1600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Overføring ", "seed" },
                    { 1603, 1600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kontoregulering", "seed" },
                    { 1604, 1600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Revolut", "seed" },
                    { 1605, 1600, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Adyen", "seed" },
                    { 1701, 1700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Nav", "seed" },
                    { 1702, 1700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Skatteetaten", "seed" },
                    { 1703, 1700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Lønn", "seed" },
                    { 1704, 1700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZTL PAYMENT", "seed" },
                    { 1705, 1700, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "LENA OG CENTRUM", "seed" },
                    { 1801, 1800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Posten Norge", "seed" },
                    { 1802, 1800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Renter", "seed" },
                    { 1803, 1800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DNB Aksje", "seed" },
                    { 1804, 1800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Omkostninger", "seed" },
                    { 1805, 1800, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Verdipapir", "seed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRules_CategoryId",
                table: "CategoryRules",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Amount",
                table: "Transactions",
                column: "Amount");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Date",
                table: "Transactions",
                column: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRules");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
