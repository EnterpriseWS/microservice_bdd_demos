using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BenefitCsBdd
{
    public class BenefitDbContext : DbContext
    {
        public DbSet<Deductible> Deductibles { get; set; }
        public DbSet<OopMax> OopMaxes { get; set; }
        public DbSet<Claim> Claims { get; set; }

        public BenefitDbContext() { }

        public BenefitDbContext(DbContextOptions<BenefitDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //=> optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["BenefitDbConnect"].ConnectionString);
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["BenefitDbConnect"].ConnectionString);
            }
        }
    }
}