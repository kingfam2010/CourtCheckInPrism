using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace CourtCheckInPrism.Droid
{
    [Service(Label = "BackGroundService")]
    public class BackGroundService : Service
    {
        bool isServiceRunning = true;
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Device.StartTimer(TimeSpan.FromMinutes(1), () =>
            {
                MessagingCenter.Send<string>("1", "geoService");
                return isServiceRunning;
            });

            return StartCommandResult.Sticky;
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            StopSelf();
            isServiceRunning = false;
            base.OnDestroy();
        }
    }
}