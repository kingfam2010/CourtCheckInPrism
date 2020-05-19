using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;
using System.Windows.Input;
using Xamarin.Forms;
using SQLite;

namespace CourtCheckInPrism.ViewModels
{
    public class AddVisitViewModel : AppMapViewModelBase
    {
        //private string OccurenceNo;
        //public string occurenceNo
        //{
        //    get { return OccurenceNo; }
        //    set { SetProperty(ref OccurenceNo, value); }
        //}

        //private string NameOfAccused;
        //public string nameOfAccused
        //{
        //    get { return NameOfAccused; }
        //    set { SetProperty(ref NameOfAccused, value); }
        //}

        //private DateTime DateOfCourtAppearence;
        //public DateTime dateOfCourtAppearence
        //{
        //    get { return DateOfCourtAppearence; }
        //    set { SetProperty(ref DateOfCourtAppearence, value); }
        //}

        //private DateTime DateOfOffence;
        //public DateTime dateOfOffence
        //{
        //    get { return DateOfOffence; }
        //    set { SetProperty(ref DateOfOffence, value); }
        //}

        //private DateTime _timePicker;
        //public DateTime TimePicker
        //{
        //    get { return _timePicker; }
        //    set { SetProperty(ref _timePicker, value); }
        //}


        public ICommand AddVisit_Command { get; set; }
        protected INavigationService _navigationService { get; }

        public AddVisitViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;
            AddVisit_Command = new Command(OnSubmit);
        }

        public async void OnSubmit()
        {            
            await _navigationService.NavigateAsync("/MasterPage/NavigationPage/TabPage");
        }
    }
}
