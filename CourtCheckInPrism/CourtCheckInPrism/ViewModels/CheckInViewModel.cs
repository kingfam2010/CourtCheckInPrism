using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;
using Prism.AppModel;
namespace CourtCheckInPrism.ViewModels
{
    public class CheckInViewModel : AppMapViewModelBase, IApplicationLifecycleAware
    {
        public CheckInViewModel(INavigationService navigationService) : base (navigationService)
        {
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
