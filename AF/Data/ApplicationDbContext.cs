
    using Microsoft.EntityFrameworkCore;
    using AF.Models;

    namespace AF.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Donation> Donations { get; set; }
            public DbSet<Fundraising> Fundraising { get; set; }
            public DbSet<ContactUs> ContactUs { get; set; }
            public DbSet<Article> Article { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Add any custom configurations (if needed)
                // e.g., modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            }
        }
    }

