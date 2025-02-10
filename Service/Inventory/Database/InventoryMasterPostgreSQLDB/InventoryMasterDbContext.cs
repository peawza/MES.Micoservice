using System;
using System.Collections.Generic;
using InventoryMasterPostgreSQLDB.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryMasterPostgreSQLDB;

public partial class InventoryMasterDbContext : DbContext
{
    public InventoryMasterDbContext()
    {
    }

    public InventoryMasterDbContext(DbContextOptions<InventoryMasterDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=202.44.229.93;Database=MES-Inventory;Username=postgres;Password=Sa@Dmin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
