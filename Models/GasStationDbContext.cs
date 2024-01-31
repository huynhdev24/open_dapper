using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GasStationDbContext : DbContext
    {
        //public GasStationDbContext()
        //{

        //}

        //public GasStationDbContext(DbContextOptions<GasStationDbContext>) : base(options)
        //{

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=192.168.1.1;Database=gasstation_huynhnv;User Id=sa;Password=Hpbvn123;MultipleActiveResultSets=true")
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserDTO>(entity =>
        //    {
        //        entity.Property(e => e.Email)
        //                .IsRequired()
        //                .HasMaxLength(100)
        //                .IsUnicode(false);
        //        entity.Property(e => e.Password)
        //                .IsRequired()
        //                .HasMaxLength(100)
        //                .IsUnicode(false);
        //        entity.Property(e => e.UserType)
        //                .IsRequired()
        //                .HasMaxLength(5)
        //                .IsUnicode();
        //    });
        //}
    }
}
