using System;
using System.Collections.Generic;

namespace InventoryMasterPostgreSQLDB.Models;

public partial class TmLocation
{
    public int Locationid { get; set; }

    public string Locationcode { get; set; } = null!;

    public string Locationname { get; set; } = null!;

    public int Areaid { get; set; }

    public bool Rm { get; set; }

    public bool Fg { get; set; }

    public bool Isactive { get; set; }

    public DateTime? Inactivedate { get; set; }

    public DateTime? Createdate { get; set; }

    public string? Createby { get; set; }

    public DateTime? Updatedate { get; set; }

    public string? Updateby { get; set; }
}
