using BookBeing.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookBeing.Data
{
    public class BookBeingDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookBeingDbContext(DbContextOptions<BookBeingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Announcement>()
    .HasOne(a => a.Library)
    .WithMany(l => l.Announcements)
    .HasForeignKey(a => a.LibraryId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Library>()
    .HasOne(l => l.User)
    .WithOne()
    .HasForeignKey<Library>(l => l.UserId)
    .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Book>()
                    .Property(b => b.Price)
                    .HasColumnType("decimal(18,2)");

            builder.Entity<Book>(entity =>
            {

                entity.HasOne(b => b.User)
                    .WithMany(u => u.Books)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.UserTakenBy)
                    .WithMany(u => u.BoughtBooks)
                    .HasForeignKey(n => n.UserTakenById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //builder.Entity<Library>()
            //    .HasOne<ApplicationUser>()
            //    .WithOne()
            //    .HasForeignKey<Library>(l => l.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Announcement>()
            //    .HasOne<Library>()
            //    .WithMany(l => l.Announcements)
            //    .HasForeignKey(a => a.LibraryId)
            //    .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
