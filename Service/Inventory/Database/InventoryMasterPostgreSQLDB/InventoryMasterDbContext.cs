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

    public virtual DbSet<TmArea> TmAreas { get; set; }

    public virtual DbSet<TmItem> TmItems { get; set; }

    public virtual DbSet<TmLocation> TmLocations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=202.44.229.93;Database=MES-Inventory;Username=postgres;Password=Sa@Dmin", b => b.MigrationsAssembly("InventoryMasterPostgreSQLDB"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TmArea>(entity =>
        {
            entity.HasKey(e => e.Areaid).HasName("idx_183395_pk_tm_area_1");

            entity.ToTable("tm_area");

            entity.HasIndex(e => e.Areacode, "areacode").IsUnique();

            entity.Property(e => e.Areaid)
                .ValueGeneratedNever()
                .HasColumnName("areaid");
            entity.Property(e => e.Areacode).HasColumnName("areacode");
            entity.Property(e => e.Areaname).HasColumnName("areaname");
            entity.Property(e => e.Areatype).HasColumnName("areatype");
            entity.Property(e => e.Createby).HasColumnName("createby");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdate");
            entity.Property(e => e.Inactivedate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("inactivedate");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Plantid).HasColumnName("plantid");
            entity.Property(e => e.Remark).HasColumnName("remark");
            entity.Property(e => e.Updateby).HasColumnName("updateby");
            entity.Property(e => e.Updatedate).HasColumnName("updatedate");
            entity.Property(e => e.Warehouseid).HasColumnName("warehouseid");
        });

        modelBuilder.Entity<TmItem>(entity =>
        {
            entity.HasKey(e => e.Itemid).HasName("idx_183395_pk_tm_item_1");

            entity.ToTable("tm_item");

            entity.HasIndex(e => e.Itemcode, "itemcode").IsUnique();

            entity.Property(e => e.Itemid)
                .ValueGeneratedNever()
                .HasColumnName("itemid");
            entity.Property(e => e.Createby).HasColumnName("createby");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdate");
            entity.Property(e => e.Inactivedate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("inactivedate");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Itemcode).HasColumnName("itemcode");
            entity.Property(e => e.Itemgroup).HasColumnName("itemgroup");
            entity.Property(e => e.Itemnameeng).HasColumnName("itemnameeng");
            entity.Property(e => e.Itemnamethai).HasColumnName("itemnamethai");
            entity.Property(e => e.Itemsubtype).HasColumnName("itemsubtype");
            entity.Property(e => e.Itemtype).HasColumnName("itemtype");
            entity.Property(e => e.Partname).HasColumnName("partname");
            entity.Property(e => e.Partno).HasColumnName("partno");
            entity.Property(e => e.Stdsnp).HasColumnName("stdsnp");
            entity.Property(e => e.Stdunit).HasColumnName("stdunit");
            entity.Property(e => e.Updateby).HasColumnName("updateby");
            entity.Property(e => e.Updatedate).HasColumnName("updatedate");
        });

        modelBuilder.Entity<TmLocation>(entity =>
        {
            entity.HasKey(e => e.Locationid).HasName("idx_183395_pk_tm_location_1");

            entity.ToTable("tm_location");

            entity.HasIndex(e => e.Locationcode, "locationcode").IsUnique();

            entity.Property(e => e.Locationid)
                .ValueGeneratedNever()
                .HasColumnName("locationid");
            entity.Property(e => e.Areaid).HasColumnName("areaid");
            entity.Property(e => e.Createby).HasColumnName("createby");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdate");
            entity.Property(e => e.Fg).HasColumnName("fg");
            entity.Property(e => e.Inactivedate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("inactivedate");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Locationcode).HasColumnName("locationcode");
            entity.Property(e => e.Locationname).HasColumnName("locationname");
            entity.Property(e => e.Rm).HasColumnName("rm");
            entity.Property(e => e.Updateby).HasColumnName("updateby");
            entity.Property(e => e.Updatedate).HasColumnName("updatedate");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
