using System;
using Microsoft.EntityFrameworkCore;

namespace GatewaysDomain.Models
{
    public partial class GatewayContext : DbContext
    {
        public GatewayContext(DbContextOptions<GatewayContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Gateway> Gateways { get; set; }
        public virtual DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gateway>(entity =>
            {
                entity.HasIndex(e => e.SerialNumber).IsUnique();
            });
        }

    }
}
