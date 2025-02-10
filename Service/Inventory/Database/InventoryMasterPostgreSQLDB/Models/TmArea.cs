using System;
using System.Collections.Generic;

namespace InventoryMasterPostgreSQLDB.Models;

public partial class TmArea
{
    public int Areaid { get; set; }

    public string Areacode { get; set; } = null!;

    public string Areaname { get; set; } = null!;

    public int Areatype { get; set; }

    public int Plantid { get; set; }

    public int Warehouseid { get; set; }

    public string Remark { get; set; } = null!;

    public bool Isactive { get; set; }

    public DateTime? Inactivedate { get; set; }

    public DateTime? Createdate { get; set; }

    public string? Createby { get; set; }

    public DateTime? Updatedate { get; set; }

    public string? Updateby { get; set; }
}
