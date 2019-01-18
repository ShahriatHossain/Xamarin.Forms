using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pigeon.DataAccess.Entities;
using Pigeon.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.WebApi.WebApi
{
    public class SecurityDbContext : IdentityDbContext<PigeonUser>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
            : base(options) 
        {            
        } 
        public DbSet<PricingType> PricingTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PricingType>().HasDiscriminator<int>("Id")
                .HasValue<FreePricingType>(1)
                .HasValue<StandardPricingType>(2)
                .HasValue<PlusPricingType>(3); 
            
            builder.Entity<PigeonUser>().ToTable("PigeonUser");            
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}