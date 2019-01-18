using Microsoft.EntityFrameworkCore;

namespace Pigeon.DataAccess.Entities
{
    public class PigeonDbContext : DbContext
    {
        public PigeonDbContext(DbContextOptions<PigeonDbContext> options)
           : base(options)
        {

        }


        public DbSet<Channel> Channels { get; set; }

        public DbSet<Institute> Institutes { get; set; }

        public DbSet<Notice> Notices { get; set; }
        public DbSet<ChannelSubscribe> ChannelSubscribes { get; set; }
        public DbSet<NoticeVoting> NoticeVotings { get; set; }
        public DbSet<UserInstitute> UserInstitutes { get; set; }

        public DbSet<InstituteCategory> InstituteCategories { get; set; }
        public DbSet<InstituteType> InstituteTypes { get; set; }
        public DbSet<ChannelCategory> ChannelCategories { get; set; }
        public DbSet<InstituteSubscribe> InstituteSubscribes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChannelSubscribe>().Ignore(t => t.Id)
                .HasKey(t => new { t.ChannelId, t.UserId });

            modelBuilder.Entity<Channel>()
                .HasMany(b => b.ChannelSubscribes)
                .WithOne();

            modelBuilder.Entity<NoticeVoting>().Ignore(t => t.Id)
                .HasKey(t => new { t.ChannelId, t.NoticeId, t.UserId }); ;

            modelBuilder.Entity<DomainInstitute>().Ignore(t => t.Id)
                .HasKey(t => new { t.DomainName }); ;

            modelBuilder.Entity<UserInstitute>().Ignore(t => t.Id)
                .HasKey(t => new { t.UserId, t.InstituteId });

            modelBuilder.Entity<InstituteSubscribe>().Ignore(t => t.Id)
                .HasKey(t => new { t.InstituteId, t.UserId });

            modelBuilder.Entity<Institute>()
                .HasMany(b => b.InstituteSubscribes)
                .WithOne();

            modelBuilder.Entity<PricingType>().HasDiscriminator<int>("Id")
                .HasValue<FreePricingType>(1)
                .HasValue<StandardPricingType>(2)
                .HasValue<PlusPricingType>(3);

            modelBuilder.Entity<Notice>()
              .HasDiscriminator<int>("NoticeType")
             .HasValue<TextNotice>(1)
             .HasValue<MediaNotice>(2);

            base.OnModelCreating(modelBuilder);
        }

    }
}
