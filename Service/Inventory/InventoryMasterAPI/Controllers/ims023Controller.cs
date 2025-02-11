using Microsoft.AspNetCore.Mvc;
using InventoryMasterAPI.Services;
using static InventoryMasterAPI.Models.IMS020;
namespace Authentication.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    public class ims023Controller : ControllerBase
    {
        private readonly IIms020Service _ims020_Service;
        private readonly ILogger<ims020Controller> logger;

        public ims023Controller(
            IIms020Service ims020_Service,
            ILogger<ims020Controller> logger
        )
        {
            this._ims020_Service = ims020_Service;
            this.logger = logger;
        }



        [HttpPost]
        [Route("insert")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> IMS023Insert([FromBody] List<IMS020_Insert_Model> info)
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





    }
}