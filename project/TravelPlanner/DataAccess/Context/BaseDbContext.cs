using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Excursion> Excursions { get; set; }

    //error fix
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Excursion>()
            .Property(e => e.Cost)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Trip>()
            .Property(t => t.Budget)
            .HasColumnType("decimal(18,2)");
    }
}
