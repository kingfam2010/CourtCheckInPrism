using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;

namespace CourtCheckInPrism.ViewModels
{
    public class CheckInViewModel : AppMapViewModelBase
    {


        public CheckInViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
