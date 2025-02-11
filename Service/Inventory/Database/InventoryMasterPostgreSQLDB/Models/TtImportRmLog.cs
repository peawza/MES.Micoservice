using System;
using System.Collections.Generic;

namespace InventoryMasterPostgreSQLDB.Models;

public partial class TtImportRmLog
{
    public int Id { get; set; }

    public string JobLogId { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public string ItemNameThai { get; set; } = null!;

    public string ItemNameEng { get; set; } = null!;

    public string? PartNo { get; set; }

    public string? PartName { get; set; }

    public string ItemType { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal Weight { get; set; }

    public decimal ShelfLife { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }
}
