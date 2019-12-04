using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;
using Prism.AppModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CourtCheckInPrism.ViewModels
{
    public class CheckInViewModel : AppMapViewModelBase, IApplicationLifecycleAware
    {
        public ICommand saveCheckOut_Command { get; set; }
        protected INavigationService _navigationService { get; }
        public ICommand save_Command { get; set; }


        public CheckInViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;
            saveCheckOut_Command = new Command(OnSubmit);
            save_Command = new Command(OnCheckin);
        }

        private async void OnCheckin(object obj)
        {
            await _navigationService.NavigateAsync("/MasterPage/NavigationPage/TabPage");
        }

        private async void OnSubmit(object obj)
        {
            await _navigationService.NavigateAsync("/MasterPage/NavigationPage/TabPage");
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
