using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).ValueGeneratedOnAdd();

                x.Property(p => p.Address).IsRequired(false).HasMaxLength(250);
                x.Property(p => p.Author).IsRequired(true).HasMaxLength(100);
                x.Property(p => p.Description).IsRequired(false).HasMaxLength(500);
                x.Property(p => p.Name).IsRequired(true).HasMaxLength(100);
                x.Property(p => p.Title).IsRequired(true).HasMaxLength(100);
                x.Property(p => p.PublishDate).IsRequired(true).HasColumnType("datetime");
            });
            builder.Entity<Member>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).ValueGeneratedOnAdd();

                x.Property(p => p.Address).IsRequired(true).HasMaxLength(250);
                x.Property(p => p.Name).IsRequired(true).HasMaxLength(100);
                x.Property(p => p.Email).IsRequired(true).HasMaxLength(100);
                x.Property(p => p.Phone).IsRequired(false).HasMaxLength(20);
            });
            builder.Entity<Barow>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).ValueGeneratedOnAdd();
                x.Property(p => p.StartDate).IsRequired(true).HasColumnType("datetime");
                x.Property(p => p.EndDate).IsRequired(true).HasColumnType("datetime");

                x.HasOne(p => p.Book)
                    .WithMany(b => b.Barows)
                    .HasForeignKey(b => b.BookId)
                    .OnDelete(DeleteBehavior.Restrict);


                x.HasOne(p => p.Member)
                  .WithMany(m => m.Barows)
                  .HasForeignKey(b => b.MemberId)
                  .OnDelete(DeleteBehavior.Restrict);

            });

            base.OnModelCreating(builder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Barow> Barows { get; set; }
    }
}