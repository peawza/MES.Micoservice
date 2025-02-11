using System;
using System.Collections.Generic;

namespace InventoryMasterPostgreSQLDB.Models;

public partial class TtImportLog
{
    public string JobLogId { get; set; } = null!;

    public string InterfaceCode { get; set; } = null!;

    public string FtpServerName { get; set; } = null!;

    public string FileFolder { get; set; } = null!;

    public string InterfaceFileName { get; set; } = null!;

    public string JobStatus { get; set; } = null!;

    public string ProcessBy { get; set; } = null!;

    public DateTime? StartDateTime { get; set; }

    public DateTime? FinishDateTime { get; set; }

    public int TotalRecord { get; set; }

    public string Remark { get; set; } = null!;
}
