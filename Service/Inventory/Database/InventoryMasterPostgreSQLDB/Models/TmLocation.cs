using System;
using System.Collections.Generic;

namespace InventoryMasterPostgreSQLDB.Models;

public partial class TmLocation
{
    public int LocationId { get; set; }

    public string LocationCode { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public int AreaId { get; set; }

    public bool RmFlag { get; set; }

    public bool FgFlag { get; set; }

    public bool WipFlag { get; set; }

    public bool ActiveFlag { get; set; }

    public bool DeleteFlag { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
