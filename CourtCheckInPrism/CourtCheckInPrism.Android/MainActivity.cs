using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;
using Shiny;
using Xamarin;

namespace CourtCheckInPrism.Droid
{
    [Activity(Label = "Court Check-In", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            FormsMaps.Init(this, bundle);
            
            //Geofencing
            

            //For permissions plugin 
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
            Shiny.Notifications.NotificationManager.TryProcessIntent(this.Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Shiny.Notifications.NotificationManager.TryProcessIntent(intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            AndroidShinyHost.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
        }
    }
}

