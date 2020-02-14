using Shiny.Jobs;
using Shiny.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtCheckInPrism
{
    public class NotificationJob : IJob
    {
        readonly INotificationManager notifications;

        public NotificationJob(INotificationManager notifications)
        {
            this.notifications = notifications;
        }
        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            await this.notifications.Send("Jobs", "Hi from Jobs");
            return true;
        }
    }
}
