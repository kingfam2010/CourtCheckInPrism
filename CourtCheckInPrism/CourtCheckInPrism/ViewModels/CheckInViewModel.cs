using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;
using Prism.AppModel;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using System.Threading.Tasks;
using Plugin.Geofencing;
using System.Linq;
//using CourtCheckInPrism.Interfaces;

namespace CourtCheckInPrism.ViewModels
{
    public class CheckInViewModel : AppMapViewModelBase, IApplicationLifecycleAware
    {
        //public static ILocalNotifications LocalNotifications { get; } = DependencyService.Get<ILocalNotifications>();
        public ICommand saveCheckOut_Command { get; set; }
        protected INavigationService _navigationService { get; }
        public ICommand save_Command { get; set; }

        public ICommand GeofenceStart_Command { get; set; }
        public ICommand GeofenceEnd_Command { get; set; }
        
        public CheckInViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;
            saveCheckOut_Command = new Command(OnSubmit);
            save_Command = new Command(OnCheckin);
            GeofenceStart_Command = new Command(OnGeofenceStart);
            GeofenceEnd_Command = new Command(OnGeofenceEnd);
        }

        private void OnGeofenceEnd(object obj)
        {
            CrossGeofencing.Current.StopMonitoring(
                    new GeofenceRegion("HeadOffice",
                        new Position(43.6605424, -79.7270547),
                        Distance.FromMeters(200)));

            if (!CrossGeofencing.Current.MonitoredRegions.Any())
                MessagingCenter.Send("", "StopGeofencingService", "");
        }

        private void OnGeofenceStart(object obj)
        {
           
            try
            {
                //var monitoredRegions = CrossGeofencing.Current.MonitoredRegions;

                CrossGeofencing.Current.StartMonitoring(new GeofenceRegion(
                        "HeadOffice",
                        new Position(43.6605424, -79.7270547),
                        Distance.FromMeters(200)
                    ));
                MessagingCenter.Send("", "StartGeofencingService", "");
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine($"Error in starting Geofence: {ex.Message}");
            }
        }

        //private void Current_RegionStatusChanged(object sender, GeofenceStatusChangedEventArgs e)
        //{
        //    var geofencePlaceId = e.Region.Identifier;
        //    var entered = e.Status == Plugin.Geofencing.GeofenceStatus.Entered;
        //    var text = entered ? "entered" : "exited";
        //    LocalNotifications.Show("Geofence update", $"{geofencePlaceId} {text}");
        //}

        private void OnCheckin(object obj)
        {

            Device.StartTimer(TimeSpan.FromSeconds(2), () => {
                _navigationService.NavigateAsync("/MasterPage/NavigationPage/TabPage");
                return false;
            });
        }

        private void OnSubmit(object obj)
        {
            Device.StartTimer(TimeSpan.FromSeconds(2), () => {
               _navigationService.NavigateAsync("/MasterPage/NavigationPage/TabPage");
                return false;
            });
            
        }

        public void OnResume()
        {
            throw new NotImplementedException();
        }

        public void OnSleep()
        {
            throw new NotImplementedException();
        }

        async Task<bool> CheckLocationPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }
                if (status == PermissionStatus.Granted)
                    return true;
                else if (status != PermissionStatus.Unknown)
                    return false;

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking permissions : {ex.Message}");
                return false;
            }
        }
    }
}
