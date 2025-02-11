using System;
using System.Collections.Generic;

namespace InventoryMasterPostgreSQLDB.Models;

public partial class TtImportLogDetail
{
    public int Id { get; set; }

    public string JobLogId { get; set; } = null!;

    public int RowNo { get; set; }

    public string ErrorDetail { get; set; } = null!;
}
