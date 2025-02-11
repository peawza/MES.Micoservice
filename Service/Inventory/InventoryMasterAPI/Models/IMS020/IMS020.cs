using System.ComponentModel.DataAnnotations;

namespace InventoryMasterAPI.Models
{
    public class IMS020
    {

        #region IMS020_Search
        public class IMS020_Search_Criteria
        {
            public string? Plant { get; set; }
            public string? Warehouse { get; set; }
            public string? Area { get; set; }
            public string? Location { get; set; }
            public bool? Status { get; set; }
        }

        public class IMS020_Search_Result
        {
            public int LocationId { get; set; }

            public string LocationCode { get; set; } = null!;

            public string LocationName { get; set; } = null!;

            public int AreaId { get; set; }

            public string AreaCode { get; set; } = null!;

            public int WarehouseId { get; set; }

            public string WarehouseCode { get; set; } = null!;

            public bool RmFlag { get; set; }

            public bool FgFlag { get; set; }

            public bool ActiveFlag { get; set; }

            public bool DeleteFlag { get; set; }

            public DateTime? CreatedDate { get; set; }

            public string? CreatedBy { get; set; }

            public DateTime? UpdatedDate { get; set; }

            public string? UpdatedBy { get; set; }
        }
        #endregion




        #region IMS020_Insert
        public class IMS020_Insert_Model
        {
            public int LocationId { get; set; }

            [Required]
            public string LocationCode { get; set; }

            [Required]
            public string? LocationName { get; set; }

            public int AreaId { get; set; }

            public bool RmFlag { get; set; }

            public bool FgFlag { get; set; }

            public bool WipFlag { get; set; }

            public bool ActiveFlag { get; set; }

            [Required]
            public string CreatedBy { get; set; }
        }

        public class IMS020_Insert_Result
        {
            public string? StatusCode { get; set; }
            public string? StatusName { get; set; }
        }
        #endregion


        #region IMS020_Delete
        public class IMS020_Delete_Criteria
        {
            public int LocationId { get; set; }
            public string? DeletedBy { get; set; }

        }

        public class IMS020_Delete_Result
        {
            public string? StatusCode { get; set; }
            public string? StatusName { get; set; }
        }
        #endregion
    }
}
