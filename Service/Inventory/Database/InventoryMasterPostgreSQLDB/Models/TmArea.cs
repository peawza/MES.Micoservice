using System;
using System.Collections.Generic;

namespace InventoryMasterPostgreSQLDB.Models;

public partial class TmArea
{
    public int AreaId { get; set; }

    public string AreaCode { get; set; } = null!;

    public string AreaName { get; set; } = null!;

    public int AreaType { get; set; }

    public int PlantId { get; set; }

    public int WarehouseId { get; set; }

    public string Remark { get; set; } = null!;

    public bool ActiveFlag { get; set; }

    public bool DeleteFlag { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
