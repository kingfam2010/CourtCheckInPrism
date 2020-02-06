using Microsoft.Extensions.DependencyInjection;
using Shiny;
using System;
using System.Collections.Generic;
using System.Text;
using Shiny.Locations;

namespace CourtCheckInPrism.Helper
{
    public class BackgroundStartup : Shiny.ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.UseNotifications(true);
            services.UseGeofencing<MyGeofenceDelegate>();

        }
    }
}
