
using InventoryMasterAPI.Repositories;
using InventoryMasterAPI.Services;
using Utils.Services;

namespace InventoryMasterAPI
{
    public class StartupService
    {
        public static void InitialService(IServiceCollection services)
        {
            /* --- Repositories --- */

            services.AddTransient<IEmailService, EmailService>();
            //EmailService : IEmailService

            services.AddScoped<IIMS020Repository, IMS020Repository>();

            /* --- Services --- */
            services.AddTransient<IIms020Service, IMS020Service>();
        }
    }
}
