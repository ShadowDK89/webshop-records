using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class WebshopContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
=> options
.UseSqlite("Data Source=DB/webshop.db").EnableSensitiveDataLogging();

    public DbSet<Color> Colors { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Format> Formats { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<TrackList> TrackLists { get; set; }


    protected override void OnModelCreating(ModelBuilder mb)
    {
        /* Configure OrderProduct Table */
        mb.Entity<OrderProduct>()
        .HasKey(t => new { t.OrderId, t.ProductId, t.ColorId, t.FormatId });

        mb.Entity<OrderProduct>()
        .HasOne(op => op.Product)
        .WithMany(p => p.OrderProducts)
        .HasForeignKey(op => op.ProductId);

        mb.Entity<OrderProduct>()
        .HasOne(op => op.Order)
        .WithMany(o => o.OrderProducts)
        .HasForeignKey(op => op.OrderId);

        /* Configure ProductColor Table */
        mb.Entity<Product>()
            .HasMany(p => p.Color)
            .WithMany(c => c.Products)
            .UsingEntity<ProductColor>(
                j => j
                    .HasOne(pc => pc.Color)
                    .WithMany(c => c.ProductColors)
                    .HasForeignKey(pc => pc.ColorId),
                j => j
                    .HasOne(pc => pc.Product)
                    .WithMany(p => p.ProductColor)
                    .HasForeignKey(pc => pc.ProductId),
                j =>
                {
                    j.HasKey(t => new { t.ProductId, t.ColorId });
                }
            );

        /* Configure ProductFormat Table */
        mb.Entity<Product>()
            .HasMany(p => p.Format)
            .WithMany(p => p.Products)
            .UsingEntity<ProductFormat>(
                j => j
                    .HasOne(pf => pf.Format)
                    .WithMany(f => f.ProductFormats)
                    .HasForeignKey(pf => pf.FormatId),
                j => j
                    .HasOne(pf => pf.Product)
                    .WithMany(p => p.ProductFormat)
                    .HasForeignKey(pf => pf.ProductId),
                j =>
                {
                    j.HasKey(t => new { t.ProductId, t.FormatId });
                }
            );

        /* Configure ProductGenre Table */
        mb.Entity<Product>()
            .HasMany(p => p.Genre)
            .WithMany(p => p.Products)
            .UsingEntity<ProductGenre>(
                j => j
                    .HasOne(pg => pg.Genre)
                    .WithMany(g => g.ProductGenres)
                    .HasForeignKey(pg => pg.GenreId),
                j => j
                    .HasOne(pg => pg.Product)
                    .WithMany(p => p.ProductGenre)
                    .HasForeignKey(pg => pg.ProductId),
                j =>
                {
                    j.HasKey(t => new { t.ProductId, t.GenreId });
                }
            );

        /* Mockdata */
        mb.Entity<Customer>().HasData(new Customer()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Adress = "Gatan 123",
            ZipCode = "123456",
            City = "Stockholm"
        }, new Customer
        {
            Id = 2,
            FirstName = "Harald",
            LastName = "Johnson",
            Adress = "Gatan 456",
            ZipCode = "654321",
            City = "Göteborg"
        });

        mb.Entity<Genre>().HasData(new Genre()
        {
            Id = 1,
            GenreName = "Heavy Metal"
        }, new Genre()
        {
            Id = 2,
            GenreName = "Power Metal"
        }, new Genre()
        {
            Id = 3,
            GenreName = "Thrash Metal"
        }, new Genre()
        {
            Id = 4,
            GenreName = "Death Metal"
        }, new Genre()
        {
            Id = 5,
            GenreName = "Proggressive Metal"
        });

        mb.Entity<Format>().HasData(new Format()
        {
            Id = 1,
            Name = "CD"
        }, new Format()
        {
            Id = 2,
            Name = "Vinyl"
        });

        mb.Entity<Color>().HasData(new Color()
        {
            Id = 1,
            Name = "Red"
        }, new Color()
        {
            Id = 2,
            Name = "Turquoise"
        }, new Color()
        {
            Id = 3,
            Name = "Yellow"
        }, new Color()
        {
            Id = 4,
            Name = "Transparent"
        });

        mb.Entity<Product>().HasData(new Product()
        {
            Id = 1,
            Artist = "Eleine",
            Album = "Dancing In Hell",
            Description = "Eleine releases their third album, a most to listen moment!",
            Genre = new List<Genre>(),
            InStock = 200,
            Price = 250,
            Color = new List<Color>(),
            Format = new List<Format>(),
            TrackList = new List<TrackList>(),
            ImgUrl = "https://images-na.ssl-images-amazon.com/images/I/81XGcFWuf-L._SL1200_.jpg",
            SpotifyLink = "https://open.spotify.com/album/0T4ce8P9dnNoA1Nud6Krbq?si=qjZITerQTOGwLuCw3FseqQ",
            ReleaseDate = new System.DateTime().AddYears(2021).AddMonths(01).AddDays(10),
        }, new Product()
        {
            Id = 2,
            Artist = "Gloryhammer",
            Album = "Legends from Beyond the Galactic Terrorvex",
            Description = "Angus McFife is on a new adveture in this awesomepacked album.",
            Genre = new List<Genre>(),
            InStock = 500,
            Price = 260,
            Color = new List<Color>(),
            Format = new List<Format>(),
            TrackList = new List<TrackList>(),
            ImgUrl = "https://upload.wikimedia.org/wikipedia/en/4/4d/Gloryhammer_-_Legends_from_Beyond_the_Galactic_Terrorvortex.jpg",
            SpotifyLink = "https://open.spotify.com/album/5EqtPIx96U0AUSzHUu4QGp?si=gZxU3_WvR-qfjMQsUG4t2Q",
            ReleaseDate = new System.DateTime().AddYears(2018).AddMonths(06).AddDays(05),
        }, new Product()
        {
            Id = 3,
            Artist = "Amaranthe",
            Album = "Manifest",
            Description = "Some claim this is the best release from Amaranthe so far. Havn't heard it yet? What are you waiting for!?",
            Genre = new List<Genre>(),
            InStock = 700,
            Price = 350,
            Color = new List<Color>(),
            Format = new List<Format>(),
            TrackList = new List<TrackList>(),
            ImgUrl = "https://upload.wikimedia.org/wikipedia/en/1/17/Amaranthe_-_Manifest.png",
            SpotifyLink = "https://open.spotify.com/album/0i8Xkm6i0Ej627KFK7GqJa?si=mvra_NDERumEccvYPd4Ryg",
            ReleaseDate = new System.DateTime().AddYears(2020).AddMonths(06).AddDays(04),
        });
        mb.Entity<TrackList>().HasData(new TrackList
        {
            Id = 1,
            SongTitle = "Enemies",
            TrackNumber = 1,
            ProductId = 1
        }, new TrackList
        {
            Id = 2,
            SongTitle = "Dancing in Hell",
            TrackNumber = 2,
            ProductId = 1
        }, new TrackList
        {
            Id = 3,
            SongTitle = "Ava Of Death",
            TrackNumber = 3,
            ProductId = 1
        }, new TrackList
        {
            Id = 4,
            SongTitle = "Into the Terrorvortex of Kor-Virliath",
            TrackNumber = 1,
            ProductId = 2
        }, new TrackList
        {
            Id = 5,
            SongTitle = "The Siege of Dunkeld (In Hoots We Trust)",
            TrackNumber = 2,
            ProductId = 2
        }, new TrackList
        {
            Id = 6,
            SongTitle = "Masters Of The Galaxy",
            TrackNumber = 3,
            ProductId = 2
        }, new TrackList
        {
            Id = 7,
            SongTitle = "Fearless",
            TrackNumber = 1,
            ProductId = 3
        }, new TrackList
        {
            Id = 8,
            SongTitle = "Make It Better",
            TrackNumber = 2,
            ProductId = 3
        }, new TrackList
        {
            Id = 9,
            SongTitle = "Scream My Name",
            TrackNumber = 3,
            ProductId = 3
        }, new TrackList
        {
            Id = 10,
            SongTitle = "Crawl from the Ashes",
            TrackNumber = 4,
            ProductId = 1
        }, new TrackList
        {
            Id = 11,
            SongTitle = "As I Breathe",
            TrackNumber = 5,
            ProductId = 1
        }, new TrackList
        {
            Id = 12,
            SongTitle = "Memoriam",
            TrackNumber = 6,
            ProductId = 1
        }, new TrackList
        {
            Id = 13,
            SongTitle = "Where Your Rotting Coprse Lie (W.Y.R.C.L.)",
            TrackNumber = 7,
            ProductId = 1
        }, new TrackList
        {
            Id = 14,
            SongTitle = "All Shall Burn",
            TrackNumber = 8,
            ProductId = 1
        }, new TrackList
        {
            Id = 15,
            SongTitle = "Die from Within",
            TrackNumber = 9,
            ProductId = 1
        }, new TrackList
        {
            Id = 16,
            SongTitle = "The World We Knew",
            TrackNumber = 10,
            ProductId = 1
        }, new TrackList
        {
            Id = 17,
            SongTitle = "Die from Within - Symphonic Version",
            TrackNumber = 11,
            ProductId = 1
        }, new TrackList
        {
            Id = 18,
            SongTitle = "The Land Of Unicorns",
            TrackNumber = 4,
            ProductId = 2
        }, new TrackList
        {
            Id = 19,
            SongTitle = "Power of the Laser Dragon",
            TrackNumber = 5,
            ProductId = 2
        }, new TrackList
        {
            Id = 20,
            SongTitle = "Legendary Enchanted Jetpack",
            TrackNumber = 6,
            ProductId = 2
        }, new TrackList
        {
            Id = 21,
            SongTitle = "Gloryhammer",
            TrackNumber = 7,
            ProductId = 2
        }, new TrackList
        {
            Id = 22,
            SongTitle = "Hootsforce",
            TrackNumber = 8,
            ProductId = 2
        }, new TrackList
        {
            Id = 23,
            SongTitle = "Battle for Eternity",
            TrackNumber = 9,
            ProductId = 2
        }, new TrackList
        {
            Id = 24,
            SongTitle = "The Fires of Ancient Cosmic Destiny",
            TrackNumber = 10,
            ProductId = 2
        }, new TrackList
        {
            Id = 25,
            SongTitle = "Viral",
            TrackNumber = 4,
            ProductId = 3
        }, new TrackList
        {
            Id = 26,
            SongTitle = "Adrenaline",
            TrackNumber = 5,
            ProductId = 3
        }, new TrackList
        {
            Id = 27,
            SongTitle = "Strong",
            TrackNumber = 6,
            ProductId = 3
        }, new TrackList
        {
            Id = 28,
            SongTitle = "The Game",
            TrackNumber = 7,
            ProductId = 3
        }, new TrackList
        {
            Id = 29,
            SongTitle = "Crystalline",
            TrackNumber = 8,
            ProductId = 3
        }, new TrackList
        {
            Id = 30,
            SongTitle = "Archangel",
            TrackNumber = 9,
            ProductId = 3
        }, new TrackList
        {
            Id = 31,
            SongTitle = "BOOM!1",
            TrackNumber = 10,
            ProductId = 3
        }, new TrackList
        {
            Id = 32,
            SongTitle = "Die and Wake Up",
            TrackNumber = 11,
            ProductId = 3
        }, new TrackList
        {
            Id = 33,
            SongTitle = "Do or Die",
            TrackNumber = 21,
            ProductId = 3
        });

        mb.Entity<Order>().HasData(new Order
        {
            Id = 1,
            DateCreated = DateTime.Now,
            PaymentMethod = "Visa",
            CustomerId = 1,
            TotalPrice = 1310,
        }, new Order
        {
            Id = 2,
            DateCreated = DateTime.Now,
            PaymentMethod = "MasterCard",
            CustomerId = 2,
            TotalPrice = 750
        });

        mb.Entity<ProductColor>().HasData(new ProductColor
        {
            ProductId = 1,
            ColorId = 1
        }, new ProductColor()
        {
            ProductId = 2,
            ColorId = 2
        }, new ProductColor()
        {
            ProductId = 3,
            ColorId = 3,
        }, new ProductColor()
        {
            ProductId = 3,
            ColorId = 4
        });
        mb.Entity<ProductGenre>().HasData(new ProductGenre()
        {
            ProductId = 1,
            GenreId = 2
        }, new ProductGenre()
        {
            ProductId = 1,
            GenreId = 1
        }, new ProductGenre()
        {
            ProductId = 2,
            GenreId = 1
        }, new ProductGenre()
        {
            ProductId = 3,
            GenreId = 1
        }, new ProductGenre()
        {
            ProductId = 3,
            GenreId = 2
        });
        mb.Entity<ProductFormat>().HasData(new ProductFormat()
        {
            ProductId = 1,
            FormatId = 1
        }, new ProductFormat()
        {
            ProductId = 1,
            FormatId = 2
        }, new ProductFormat()
        {
            ProductId = 2,
            FormatId = 2
        }, new ProductFormat()
        {
            ProductId = 3,
            FormatId = 1
        }, new ProductFormat()
        {
            ProductId = 3,
            FormatId = 2
        });

        mb.Entity<OrderProduct>().HasData(new OrderProduct
        {
            OrderId = 1,
            ProductId = 3,
            Amount = 3,
            ColorId = 4,
            FormatId = 2,
            Price = 1050
        }, new OrderProduct
        {
            OrderId = 1,
            ProductId = 2,
            Amount = 1,
            ColorId = 2,
            FormatId = 1,
            Price = 260
        }, new OrderProduct
        {
            OrderId = 2,
            ProductId = 1,
            Amount = 1,
            ColorId = 1,
            FormatId = 1,
            Price = 250
        }, new OrderProduct
        {
            OrderId = 2,
            ProductId = 3,
            Amount = 2,
            ColorId = 3,
            FormatId = 2,
            Price = 500
        });



    }
}