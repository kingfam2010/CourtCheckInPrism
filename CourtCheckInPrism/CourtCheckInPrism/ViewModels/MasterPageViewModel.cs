using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;

namespace CourtCheckInPrism.ViewModels
{
    public class MasterPageViewModel : AppMapViewModelBase
    {


        public MasterPageViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
