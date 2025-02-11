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

    public virtual DbSet<TmArea> TmAreas { get; set; }

    public virtual DbSet<TmItem> TmItems { get; set; }

    public virtual DbSet<TmLocation> TmLocations { get; set; }

    public virtual DbSet<TtExportLog> TtExportLogs { get; set; }

    public virtual DbSet<TtImportLog> TtImportLogs { get; set; }

    public virtual DbSet<TtImportLogDetail> TtImportLogDetails { get; set; }

    public virtual DbSet<TtImportRmLog> TtImportRmLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=MES-Inventory;Username=postgres;Password=postdb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TmArea>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("idx_183395_pk_tm_area_1");

            entity.ToTable("tm_area");

            entity.HasIndex(e => e.AreaCode, "areacode").IsUnique();

            entity.Property(e => e.AreaId)
                .ValueGeneratedNever()
                .HasColumnName("area_id");
            entity.Property(e => e.ActiveFlag).HasColumnName("active_flag");
            entity.Property(e => e.AreaCode).HasColumnName("area_code");
            entity.Property(e => e.AreaName).HasColumnName("area_name");
            entity.Property(e => e.AreaType).HasColumnName("area_type");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.DeleteFlag).HasColumnName("delete_flag");
            entity.Property(e => e.PlantId).HasColumnName("plant_id");
            entity.Property(e => e.Remark).HasColumnName("remark");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
        });

        modelBuilder.Entity<TmItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("idx_183395_pk_tm_item_1");

            entity.ToTable("tm_item");

            entity.HasIndex(e => e.ItemCode, "itemcode").IsUnique();

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("item_id");
            entity.Property(e => e.ActiveFlag).HasColumnName("active_flag");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.DeleteFlag).HasColumnName("delete_flag");
            entity.Property(e => e.ItemCode).HasColumnName("item_code");
            entity.Property(e => e.ItemGroup).HasColumnName("item_group");
            entity.Property(e => e.ItemNameEn).HasColumnName("item_name_en");
            entity.Property(e => e.ItemNameLocal).HasColumnName("item_name_local");
            entity.Property(e => e.ItemType).HasColumnName("item_type");
            entity.Property(e => e.PartName).HasColumnName("part_name");
            entity.Property(e => e.PartNo).HasColumnName("part_no");
            entity.Property(e => e.ShelfLife).HasColumnName("shelf_life");
            entity.Property(e => e.Unit).HasColumnName("unit");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<TmLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("idx_183395_pk_tm_location_1");

            entity.ToTable("tm_location");

            entity.HasIndex(e => e.LocationCode, "locationcode").IsUnique();

            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.ActiveFlag).HasColumnName("active_flag");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.DeleteFlag).HasColumnName("delete_flag");
            entity.Property(e => e.FgFlag).HasColumnName("fg_flag");
            entity.Property(e => e.LocationCode).HasColumnName("location_code");
            entity.Property(e => e.LocationName).HasColumnName("location_name");
            entity.Property(e => e.RmFlag).HasColumnName("rm_flag");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.WipFlag).HasColumnName("wip_flag");
        });

        modelBuilder.Entity<TtExportLog>(entity =>
        {
            entity.HasKey(e => e.JobLogId).HasName("idx_pk_tt_export_log");

            entity.ToTable("tt_export_log");

            entity.Property(e => e.JobLogId).HasColumnName("job_log_id");
            entity.Property(e => e.ExportFileName).HasColumnName("export_file_name");
            entity.Property(e => e.FileFolder).HasColumnName("file_folder");
            entity.Property(e => e.FinishDateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("finish_date_time");
            entity.Property(e => e.FtpServerName).HasColumnName("ftp_server_name");
            entity.Property(e => e.InterfaceCode).HasColumnName("interface_code");
            entity.Property(e => e.JobStatus).HasColumnName("job_status");
            entity.Property(e => e.ProcessBy).HasColumnName("process_by");
            entity.Property(e => e.Remark).HasColumnName("remark");
            entity.Property(e => e.StartDateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("start_date_time");
            entity.Property(e => e.TotalRecord).HasColumnName("total_record");
        });

        modelBuilder.Entity<TtImportLog>(entity =>
        {
            entity.HasKey(e => e.JobLogId).HasName("idx_pk_tt_import_log");

            entity.ToTable("tt_import_log");

            entity.Property(e => e.JobLogId).HasColumnName("job_log_id");
            entity.Property(e => e.FileFolder).HasColumnName("file_folder");
            entity.Property(e => e.FinishDateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("finish_date_time");
            entity.Property(e => e.FtpServerName).HasColumnName("ftp_server_name");
            entity.Property(e => e.InterfaceCode).HasColumnName("interface_code");
            entity.Property(e => e.InterfaceFileName).HasColumnName("interface_file_name");
            entity.Property(e => e.JobStatus).HasColumnName("job_status");
            entity.Property(e => e.ProcessBy).HasColumnName("process_by");
            entity.Property(e => e.Remark).HasColumnName("remark");
            entity.Property(e => e.StartDateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("start_date_time");
            entity.Property(e => e.TotalRecord).HasColumnName("total_record");
        });

        modelBuilder.Entity<TtImportLogDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("idx_pk_tt_import_log_detail");

            entity.ToTable("tt_import_log_detail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ErrorDetail).HasColumnName("error_detail");
            entity.Property(e => e.JobLogId).HasColumnName("job_log_id");
            entity.Property(e => e.RowNo).HasColumnName("row_no");
        });

        modelBuilder.Entity<TtImportRmLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("idx_pk_tt_import_rm_log");

            entity.ToTable("tt_import_rm_log");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.ItemCode).HasColumnName("item_code");
            entity.Property(e => e.ItemNameEng).HasColumnName("item_name_eng");
            entity.Property(e => e.ItemNameThai).HasColumnName("item_name_thai");
            entity.Property(e => e.ItemType).HasColumnName("item_type");
            entity.Property(e => e.JobLogId).HasColumnName("job_log_id");
            entity.Property(e => e.PartName).HasColumnName("part_name");
            entity.Property(e => e.PartNo).HasColumnName("part_no");
            entity.Property(e => e.ShelfLife).HasColumnName("shelf_life");
            entity.Property(e => e.Unit).HasColumnName("unit");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
