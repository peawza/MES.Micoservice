﻿
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


            /* --- Services --- */

        }
    }
}
