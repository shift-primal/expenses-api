using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    private static readonly DateTime SeedDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static void Seed(ModelBuilder mb)
    {
        mb.Entity<Category>()
            .HasData(
                new
                {
                    Id = 100,
                    Name = "Dagligvare",
                    IsDefault = true,
                },
                new
                {
                    Id = 200,
                    Name = "Mat ute",
                    IsDefault = true,
                },
                new
                {
                    Id = 300,
                    Name = "Hjem",
                    IsDefault = true,
                },
                new
                {
                    Id = 400,
                    Name = "Underholdning",
                    IsDefault = true,
                },
                new
                {
                    Id = 500,
                    Name = "Gaming",
                    IsDefault = true,
                },
                new
                {
                    Id = 600,
                    Name = "Abonnement",
                    IsDefault = true,
                },
                new
                {
                    Id = 700,
                    Name = "Netthandel",
                    IsDefault = true,
                },
                new
                {
                    Id = 800,
                    Name = "Helse",
                    IsDefault = true,
                },
                new
                {
                    Id = 900,
                    Name = "Kosmetikk",
                    IsDefault = true,
                },
                new
                {
                    Id = 1000,
                    Name = "Kreditt",
                    IsDefault = true,
                },
                new
                {
                    Id = 1100,
                    Name = "Transport",
                    IsDefault = true,
                },
                new
                {
                    Id = 1200,
                    Name = "Bil",
                    IsDefault = true,
                },
                new
                {
                    Id = 1300,
                    Name = "Bolig",
                    IsDefault = true,
                },
                new
                {
                    Id = 1400,
                    Name = "Boutgifter",
                    IsDefault = true,
                },
                new
                {
                    Id = 1500,
                    Name = "Forsikring",
                    IsDefault = true,
                },
                new
                {
                    Id = 1600,
                    Name = "Overføring",
                    IsDefault = true,
                },
                new
                {
                    Id = 1700,
                    Name = "Inntekt",
                    IsDefault = true,
                },
                new
                {
                    Id = 1800,
                    Name = "Annet",
                    IsDefault = true,
                }
            );

        mb.Entity<CategoryRule>()
            .HasData(
                // Dagligvare (100)
                new
                {
                    Id = 101,
                    Keyword = "EXTRA",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 102,
                    Keyword = "KIWI",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 103,
                    Keyword = "REMA",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 104,
                    Keyword = "COOP",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 105,
                    Keyword = "Bunnpris",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 106,
                    Keyword = "Spar",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 107,
                    Keyword = "Joker",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 108,
                    Keyword = "Matkroken",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 109,
                    Keyword = "Cc Mat",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 110,
                    Keyword = "Mix",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 111,
                    Keyword = "Vinmonopolet",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 112,
                    Keyword = "Obs",
                    CategoryId = 100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Mat ute (200)
                new
                {
                    Id = 201,
                    Keyword = "Burger King",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 202,
                    Keyword = "Mdc",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 203,
                    Keyword = "Mcdonalds",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 204,
                    Keyword = "BK",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 205,
                    Keyword = "Little Eat",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 206,
                    Keyword = "Fly Chicken",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 207,
                    Keyword = "Expressenpizza",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 208,
                    Keyword = "Pizzabakeren",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 209,
                    Keyword = "Raufoss Pizza",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 210,
                    Keyword = "Dos Amigos",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 211,
                    Keyword = "Sander Asia Mat",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 212,
                    Keyword = "Gelatiamo",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 213,
                    Keyword = "Thai Bahn",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 214,
                    Keyword = "Wolt",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 215,
                    Keyword = "foodora",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 216,
                    Keyword = "Cafe Amsterdam",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 217,
                    Keyword = "Hygge Gjøvik",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 219,
                    Keyword = "Javel Oslo",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 220,
                    Keyword = "Totensnacks",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 221,
                    Keyword = "King Kebap",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 222,
                    Keyword = "Selecta",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 223,
                    Keyword = "Øya Maritim",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 224,
                    Keyword = "Max Burgers",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 225,
                    Keyword = "Dominos Pizza",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 226,
                    Keyword = "Nebbenes Kro",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 227,
                    Keyword = "Shell",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 228,
                    Keyword = "7-eleven",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 229,
                    Keyword = "Esso",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 230,
                    Keyword = "ST1",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Hjem (300)
                new
                {
                    Id = 301,
                    Keyword = "IKEA",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 302,
                    Keyword = "Søstrene Grene",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 303,
                    Keyword = "Biltema",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 304,
                    Keyword = "Rusta",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 305,
                    Keyword = "Jula",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 306,
                    Keyword = "Clas Ohl",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 307,
                    Keyword = "Plantasjen",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 308,
                    Keyword = "Kid",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 309,
                    Keyword = "Europris",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 310,
                    Keyword = "Normal",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 311,
                    Keyword = "Buddy",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 312,
                    Keyword = "Musti",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 313,
                    Keyword = "Jysk",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 314,
                    Keyword = "Nille",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 315,
                    Keyword = "Sport 1",
                    CategoryId = 300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 316,
                    Keyword = "Kitch",
                    CategoryId = 200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Underholdning (400)
                new
                {
                    Id = 401,
                    Keyword = "Gjøvik Kino",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 402,
                    Keyword = "Norsk Tipping",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 403,
                    Keyword = "Totenbadet",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 404,
                    Keyword = "Fjellhallen",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 405,
                    Keyword = "Geekevents",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 406,
                    Keyword = "Sveve",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 407,
                    Keyword = "Fun Padel",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 408,
                    Keyword = "Padel-senter",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 409,
                    Keyword = "Mjøsstranda",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 410,
                    Keyword = "Tikkio",
                    CategoryId = 400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Gaming (500)
                new
                {
                    Id = 501,
                    Keyword = "Steam",
                    CategoryId = 500,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 502,
                    Keyword = "discord",
                    CategoryId = 500,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 503,
                    Keyword = "riotgamesl",
                    CategoryId = 500,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 504,
                    Keyword = "jagex",
                    CategoryId = 500,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 505,
                    Keyword = "eneba",
                    CategoryId = 500,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Abonnement (600)
                new
                {
                    Id = 601,
                    Keyword = "APPLE.COM/BILL",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 602,
                    Keyword = "tidalmusic",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 603,
                    Keyword = "Telenor",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 604,
                    Keyword = "Google One",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 605,
                    Keyword = "splice",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 606,
                    Keyword = "Claude.ai",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 607,
                    Keyword = "Anthropic",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 608,
                    Keyword = "OpenAI",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 609,
                    Keyword = "Scrimba",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 610,
                    Keyword = "Real Debrid",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 611,
                    Keyword = "Suno",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 612,
                    Keyword = "elitehosting",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 613,
                    Keyword = "agderposten",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 614,
                    Keyword = "Lyse Tele",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 615,
                    Keyword = "lennardigi",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 616,
                    Keyword = "native Ins",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 617,
                    Keyword = "Netflix",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 618,
                    Keyword = "Disney Plus",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 619,
                    Keyword = "storytel",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 620,
                    Keyword = "Mova AS",
                    CategoryId = 600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Netthandel (700)
                new
                {
                    Id = 701,
                    Keyword = "CDON",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 702,
                    Keyword = "Maxgaming",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 703,
                    Keyword = "Junkyard",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 704,
                    Keyword = "proshop",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 705,
                    Keyword = "Helthjem",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 706,
                    Keyword = "Thomann",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 707,
                    Keyword = "Power",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 708,
                    Keyword = "Xxl",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 709,
                    Keyword = "Norli",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 710,
                    Keyword = "NOX PADEL",
                    CategoryId = 700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Helse (800)
                new
                {
                    Id = 801,
                    Keyword = "Spesialistsenter",
                    CategoryId = 800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 802,
                    Keyword = "Apotek",
                    CategoryId = 800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 803,
                    Keyword = "Legevakt",
                    CategoryId = 800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 804,
                    Keyword = "Helfo",
                    CategoryId = 800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 805,
                    Keyword = "Rtg.polikl",
                    CategoryId = 800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 806,
                    Keyword = "Vitus",
                    CategoryId = 800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 807,
                    Keyword = "Legesenter",
                    CategoryId = 800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 808,
                    Keyword = "Oslo Storbylege",
                    CategoryId = 800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 809,
                    Keyword = "Farmasiet",
                    CategoryId = 800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Kosmetikk (900)
                new
                {
                    Id = 901,
                    Keyword = "Cutters",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 902,
                    Keyword = "Blivakker",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 903,
                    Keyword = "Nailster",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 904,
                    Keyword = "nordicnails",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 905,
                    Keyword = "NORDICFEEL",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 906,
                    Keyword = "KICKS",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 907,
                    Keyword = "Rituals",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 908,
                    Keyword = "Feel Raufoss",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 909,
                    Keyword = "Zalando",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 910,
                    Keyword = "Bubbleroom",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 911,
                    Keyword = "CHANGE Lingerie",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 912,
                    Keyword = "LYKO",
                    CategoryId = 900,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Kreditt (1000)
                new
                {
                    Id = 1001,
                    Keyword = "Klarna",
                    CategoryId = 1000,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1002,
                    Keyword = "Qliro",
                    CategoryId = 1000,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1003,
                    Keyword = "Credicare",
                    CategoryId = 1000,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1004,
                    Keyword = "Tfbank",
                    CategoryId = 1000,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1005,
                    Keyword = "Riverty",
                    CategoryId = 1000,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1006,
                    Keyword = "ARVATO",
                    CategoryId = 1000,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Transport (1100)
                new
                {
                    Id = 1101,
                    Keyword = "Ruter",
                    CategoryId = 1100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1102,
                    Keyword = "Entur",
                    CategoryId = 1100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1103,
                    Keyword = "Crown Seaways",
                    CategoryId = 1100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1104,
                    Keyword = "VY App",
                    CategoryId = 1100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1105,
                    Keyword = "Asfinag",
                    CategoryId = 1100,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Bil (1200)
                new
                {
                    Id = 1201,
                    Keyword = "Uno-x",
                    CategoryId = 1200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1202,
                    Keyword = "Tesla Inc",
                    CategoryId = 1200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1203,
                    Keyword = "IONITY",
                    CategoryId = 1200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1204,
                    Keyword = "KOPLE",
                    CategoryId = 1200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1205,
                    Keyword = "Recharge",
                    CategoryId = 1200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1206,
                    Keyword = "Easypark",
                    CategoryId = 1200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1207,
                    Keyword = "Apcoa",
                    CategoryId = 1200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1208,
                    Keyword = "NORDEA FINANS",
                    CategoryId = 1200,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Bolig (1300)
                new
                {
                    Id = 1301,
                    Keyword = "Nedbetaling",
                    CategoryId = 1300,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Boutgifter (1400)
                new
                {
                    Id = 1401,
                    Keyword = "Fortum",
                    CategoryId = 1400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1402,
                    Keyword = "Vestre Toten Kommune",
                    CategoryId = 1400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1403,
                    Keyword = "Ringsaker Kommune",
                    CategoryId = 1400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1404,
                    Keyword = "Globalconnect",
                    CategoryId = 1400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1405,
                    Keyword = "VERISURE",
                    CategoryId = 1400,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Forsikring (1500)
                new
                {
                    Id = 1501,
                    Keyword = "Fremtind Forsikring",
                    CategoryId = 1500,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1502,
                    Keyword = "GJENSIDIGE",
                    CategoryId = 1500,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1503,
                    Keyword = "AGRIA",
                    CategoryId = 1500,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Overføring (1600)
                new
                {
                    Id = 1601,
                    Keyword = "Overføring Innland",
                    CategoryId = 1600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1602,
                    Keyword = "Overføring ",
                    CategoryId = 1600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1603,
                    Keyword = "Kontoregulering",
                    CategoryId = 1600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1604,
                    Keyword = "Revolut",
                    CategoryId = 1600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1605,
                    Keyword = "Adyen",
                    CategoryId = 1600,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Inntekt (1700)
                new
                {
                    Id = 1701,
                    Keyword = "Nav",
                    CategoryId = 1700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1702,
                    Keyword = "Skatteetaten",
                    CategoryId = 1700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1703,
                    Keyword = "Lønn",
                    CategoryId = 1700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1704,
                    Keyword = "ZTL PAYMENT",
                    CategoryId = 1700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1705,
                    Keyword = "LENA OG CENTRUM",
                    CategoryId = 1700,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                // Annet (1800)
                new
                {
                    Id = 1801,
                    Keyword = "Posten Norge",
                    CategoryId = 1800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1802,
                    Keyword = "Renter",
                    CategoryId = 1800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1803,
                    Keyword = "DNB Aksje",
                    CategoryId = 1800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1804,
                    Keyword = "Omkostninger",
                    CategoryId = 1800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                },
                new
                {
                    Id = 1805,
                    Keyword = "Verdipapir",
                    CategoryId = 1800,
                    Source = "seed",
                    CreatedAt = SeedDate,
                }
            );
    }
}
