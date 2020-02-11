using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;
using System;
using Xamarin.Forms;

namespace CourtCheckInPrism.Droid
{
    [Activity(Label = "Court Check-In", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static bool Initialized { get; private set; } = false;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            

            //For permissions (location and storage)
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));

            //Subscribing to messaging centre
            if (!Initialized)
            {
                MessagingCenter.Subscribe<string, string>(string.Empty, "StartGeofencingService", StartGeofencingService);
                MessagingCenter.Subscribe<string, string>(string.Empty, "StopGeofencingService", StopGeofencingService);
            }
            Initialized = true;
        }

        private void StopGeofencingService(string arg1, string arg2)
        {
            throw new NotImplementedException();
        }

        private void StartGeofencingService(string arg1, string arg2)
        {
            throw new NotImplementedException();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
           //Register any platform specific code here.
        }
    }
}

