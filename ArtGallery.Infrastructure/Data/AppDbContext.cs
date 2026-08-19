using BehindArt.Domain.Entitiyes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Era> Eras => Set<Era>();
        public DbSet<Painting> Paintings => Set<Painting>();
        public DbSet<Like> Likes => Set<Like>();
        public DbSet<Save> Saves => Set<Save>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Painting
            modelBuilder.Entity<Painting>()
            .HasOne(p => p.Artist)
            .WithMany(a => a.Paintings)
            .HasForeignKey(p => p.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Painting>()
            .HasOne(p => p.Era)
            .WithMany(e => e.Paintings)
            .HasForeignKey(p => p.EraId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Painting>()
            .Property(p=> p.Views)
            .HasDefaultValue(0);

            modelBuilder.Entity<Painting>()
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

            // User
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

            // Likes 
            modelBuilder.Entity<Like>()
            .HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
            .HasOne(l => l.Painting)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PaintingId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
            .HasIndex(l => new { l.UserId, l.PaintingId })
            .IsUnique();

            modelBuilder.Entity<Like>()
           .Property(l => l.CreatedAt)
           .HasDefaultValueSql("GETUTCDATE()");

            // Saves
            modelBuilder.Entity<Save>()
            .HasOne(s => s.User)
            .WithMany(u => u.Saves)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Save>()
           .HasOne(s => s.Painting)
           .WithMany(p => p.Saves)
           .HasForeignKey(s => s.PaintingId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Save>()
            .HasIndex(s => new { s.UserId, s.PaintingId })
            .IsUnique();

            modelBuilder.Entity<Save>()
                .Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");



            // Seed data
            modelBuilder.Entity<Artist>().HasData(
                new Artist
                {
                    Id = 1,
                    Name = "Leonardo da Vinci",
                    BirthYear = 1452,
                    DeathYear = 1519,
                    Biography = "Italian polymath of the High Renaissance."
                },
                new Artist
                {
                    Id = 2,
                    Name = "Vincent van Gogh",
                    BirthYear = 1853,
                    DeathYear = 1890,
                    Biography = "Dutch post-impressionist painter."
                },
                new Artist
                {
                    Id = 3,
                    Name = "Edvard Munch",
                    BirthYear = 1863,
                    DeathYear = 1944,
                    Biography = "Norwegian expressionist painter."
                }
            );

            modelBuilder.Entity<Era>().HasData(
                new Era
                {
                    Id = 1,
                    Name = "Renaissance",
                    StartYear = 1300,
                    EndYear = 1600,
                    Description = "A period of cultural rebirth in Europe."
                },
                new Era
                {
                    Id = 2,
                    Name = "Post-Impressionism",
                    StartYear = 1886,
                    EndYear = 1905,
                    Description = "Emphasis on symbolic content and geometric form."
                },
                new Era
                {
                    Id = 3,
                    Name = "Expressionism",
                    StartYear = 1905,
                    EndYear = 1933,
                    Description = "Art that presents the world from a subjective perspective."
                }
            );

            modelBuilder.Entity<Painting>().HasData(
                new Painting
                {
                    Id = 1,
                    Title = "Mona Lisa",
                    ArtistId = 1,
                    EraId = 1,
                    Year = 1503,
                    ImageUrl = "https://placehold.co/600x800?text=Mona+Lisa",
                    Description = "Portrait of a woman with an enigmatic expression."
                },
                new Painting
                {
                    Id = 2,
                    Title = "The Starry Night",
                    ArtistId = 2,
                    EraId = 2,
                    Year = 1889,
                    ImageUrl = "https://placehold.co/600x800?text=Starry+Night",
                    Description = "A swirling night sky over a quiet village."
                },
                new Painting
                {
                    Id = 3,
                    Title = "The Scream",
                    ArtistId = 3,
                    EraId = 3,
                    Year = 1893,
                    ImageUrl = "https://placehold.co/600x800?text=The+Scream",
                    Description = "An agonized figure against a blood-red sky."
                },
                new Painting
                {
                    Id = 4,
                    Title = "The Persistence of Memory",
                    ArtistId = 1,
                    EraId = 1,
                    Year = 1931,
                    ImageUrl = "https://placehold.co/600x800?text=Persistence+of+Memory",
                    Description = "Melting clocks in a dreamlike landscape."
                },
                new Painting
                {
                    Id = 5,
                    Title = "Girl with a Pearl Earring",
                    ArtistId = 2,
                    EraId = 2,
                    Year = 1665,
                    ImageUrl = "https://placehold.co/600x800?text=Girl+with+Pearl+Earring",
                    Description = "A young girl wearing an exotic dress and a pearl earring."
                }
            );
        }

    }
}
