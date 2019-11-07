using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;

namespace CourtCheckInPrism.ViewModels
{
    public class TabPageViewModel : AppMapViewModelBase
    {


        public TabPageViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
