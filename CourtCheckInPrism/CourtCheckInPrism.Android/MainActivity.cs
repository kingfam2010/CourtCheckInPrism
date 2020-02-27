using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common.Apis;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

namespace CourtCheckInPrism.Droid
{
    [Activity(Label = "Court Check-In", Icon = "@drawable/prp_logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);            

            //For permissions plugin 
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));

            //Subscribing location service
            MessagingCenter.Unsubscribe<string>(this, "locationVerficationService");
            MessagingCenter.Subscribe<string>(this, "locationVerficationService", (value)=> 
            {
                if(value == "1")
                {
                    //start Location service
                    StartService(new Intent(this, typeof(BackGroundService)));
                }
                else if(value == "0")
                {
                    //Stop Location Service
                    StopService(new Intent(this, typeof(BackGroundService)));
                }
            
            
            });
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
            // Register any platform specific implementations
        }
    }
}