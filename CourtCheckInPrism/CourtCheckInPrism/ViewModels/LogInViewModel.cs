using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using CourtCheckInPrism.Views;

namespace CourtCheckInPrism.ViewModels
{
    public class LogInViewModel : AppMapViewModelBase, INotifyPropertyChanged
    {
        protected INavigationService _navigationService { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs(Email));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs(Password));
            }
        }


        public ICommand SignIn_Command { get; set; }

        public LogInViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;
            SignIn_Command = new Command(OnSubmit);
        }

        public async void OnSubmit()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                _ = App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                if (Email == "abc@gmail.com" && Password == "1234")
                {
                    await App.Current.MainPage.DisplayAlert("Login Success", "", "Ok");
                    //Navigate to Wellcom page after successfully login  
                    await _navigationService.NavigateAsync("/MasterPage/NavigationPage/TabPage");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
            }
        }
    }
}
