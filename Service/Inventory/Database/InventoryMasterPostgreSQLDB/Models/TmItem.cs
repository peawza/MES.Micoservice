using System;
using System.Collections.Generic;

namespace InventoryMasterPostgreSQLDB.Models;

public partial class TmItem
{
    public int Itemid { get; set; }

    public string Itemcode { get; set; } = null!;

    public string Itemnamethai { get; set; } = null!;

    public string Itemnameeng { get; set; } = null!;

    public int Itemgroup { get; set; }

    public string Partno { get; set; } = null!;

    public string Partname { get; set; } = null!;

    public int Itemtype { get; set; }

    public int Itemsubtype { get; set; }

    public string Stdunit { get; set; } = null!;

    public decimal Stdsnp { get; set; }

    public bool Isactive { get; set; }

    public DateTime? Inactivedate { get; set; }

    public DateTime? Createdate { get; set; }

    public string? Createby { get; set; }

    public DateTime? Updatedate { get; set; }

    public string? Updateby { get; set; }
}
