using Microsoft.AspNetCore.Mvc;
using InventoryMasterAPI.Services;
using static InventoryMasterAPI.Models.IMS020;
namespace Authentication.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    public class ims020Controller : ControllerBase
    {
        private readonly IIms020Service _ims020_Service;
        private readonly ILogger<ims020Controller> logger;

        public ims020Controller(
            IIms020Service ims020_Service,
            ILogger<ims020Controller> logger
        )
        {
            this._ims020_Service = ims020_Service;
            this.logger = logger;
        }


        [HttpPost]
        [Route("search")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> IMS020Search([FromBody] IMS020_Search_Criteria criteria)
        {
            var results = await _ims020_Service.IMS020Search(criteria);
            return Ok(results);
        }


        [HttpPost]
        [Route("save")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> IMS020Save([FromBody] List<IMS020_Insert_Model> info)
        {
            try
            {
                var results = await _ims020_Service.IMS020Insert(info);
                return Ok(results);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("delete")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> IMS020Delete([FromBody] IMS020_Delete_Criteria criteria)
        {
            var results = await _ims020_Service.IMS020Delete(criteria);
            return Ok(results);
        }





    }
}