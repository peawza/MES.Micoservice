using System;
using System.Collections.Generic;

namespace InventoryMasterPostgreSQLDB.Models;

public partial class TmItem
{
    public int ItemId { get; set; }

    public string ItemCode { get; set; } = null!;

    public string ItemGroup { get; set; } = null!;

    public string ItemNameLocal { get; set; } = null!;

    public string ItemNameEn { get; set; } = null!;

    public string PartNo { get; set; } = null!;

    public string PartName { get; set; } = null!;

    public string ItemType { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal Weight { get; set; }

    public decimal ShelfLife { get; set; }

    public bool ActiveFlag { get; set; }

    public bool DeleteFlag { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
