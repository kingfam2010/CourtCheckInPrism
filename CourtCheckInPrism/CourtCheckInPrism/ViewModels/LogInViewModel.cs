using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;

namespace CourtCheckInPrism.ViewModels
{
    public class LogInViewModel : AppMapViewModelBase
    {


        public LogInViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
