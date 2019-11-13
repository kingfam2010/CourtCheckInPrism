using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;
using System.Windows.Input;
using Xamarin.Forms;

namespace CourtCheckInPrism.ViewModels
{
    public class VisitScheduleViewModel : AppMapViewModelBase, IActiveAware
    {
        
#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }
        protected INavigationService _navigationService { get; }
        
        public VisitScheduleViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;
           
        }
     
    }
}
