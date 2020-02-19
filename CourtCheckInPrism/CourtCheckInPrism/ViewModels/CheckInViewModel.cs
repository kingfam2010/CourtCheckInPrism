using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;
using Prism.AppModel;
using System.Windows.Input;
using Xamarin.Forms;
using Shiny;
using Shiny.Locations;
using Shiny.Notifications;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace CourtCheckInPrism.ViewModels
{
    public class CheckInViewModel : AppMapViewModelBase, IApplicationLifecycleAware
    {
        public ICommand saveCheckOut_Command { get; set; }
        protected INavigationService _navigationService { get; }
        public ICommand save_Command { get; set; }
        public ICommand fenceStart_Command { get; set; }


        public CheckInViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;
            saveCheckOut_Command = new Command(OnSubmit);
            save_Command = new Command(OnCheckin);
            fenceStart_Command = new Command(Register);
        }

        private async void Register(object obj)
        {
            try
            {
                var geofences = ShinyHost.Resolve<IGeofenceManager>();
                var notifications = ShinyHost.Resolve<INotificationManager>();

                var access = await geofences.RequestAccess();
                if (access == AccessState.Available)
                {
                    await geofences.StartMonitoring(new GeofenceRegion(
                        "CN Tower - Toronto, Canada",
                        new Position(43.6605424, -79.7270547),
                        Distance.FromMeters(100)
                    )
                    {
                        NotifyOnEntry = true,
                        NotifyOnExit = true,
                        SingleUse = false
                    });
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Need location", ex.Message, "OK");
            }

            //var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

            //if (status != PermissionStatus.Granted)
            //{
           //     if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
            //    {
            //        await Application.Current.MainPage.DisplayAlert("Need location", "Going to need that location", "OK");
                    
             //   }

             //   var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
             //   if (results.ContainsKey(Permission.Location))
             //   {
             //       status = results[Permission.Location];
             //       if (status == PermissionStatus.Granted)
             //       {
             //           await geofences.StartMonitoring(new GeofenceRegion(
             //      "CN Tower - Toronto, Canada",
             //      new Position(43.6605424, -79.7270547),
             //      Distance.FromMeters(200)
            //)
             //           {
             //               NotifyOnEntry = true,
             //               NotifyOnExit = true,
             //               SingleUse = false
             //           });
              //      }
             //   }
           // }
            
        }

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
    }
}
