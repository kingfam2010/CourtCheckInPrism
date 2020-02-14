using Microsoft.Extensions.DependencyInjection;
using Shiny;
using Shiny.Jobs;
using Shiny.Locations;
using Shiny.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CourtCheckInPrism
{
    public class NewStartup : ShinyStartup
    {
        
        public override void ConfigureServices(IServiceCollection services)
        {
            services.UseGeofencing<MyGeofenceDelegate>();
            services.UseNotifications(true);            
        }
    }

    public class MyGeofenceDelegate : IGeofenceDelegate
    {

        readonly INotificationManager notifications;

        public MyGeofenceDelegate(INotificationManager notifications)
        {
            this.notifications = notifications;
        }


        public async Task OnStatusChanged(GeofenceState newStatus, GeofenceRegion region)
        {
            if (newStatus == GeofenceState.Entered)
            {
                await this.notifications.Send(new Notification
                {
                    Title = "WELCOME!",
                    Message = "It is good to have you back " + region.Identifier
                });
            }
            else
            {
                await this.notifications.Send(new Notification
                {
                    Title = "GOODBYE!",
                    Message = "You will be missed at " + region.Identifier
                });
            }
        }
    }
}
