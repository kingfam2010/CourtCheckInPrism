using CourtCheckInPrism.ViewModels;
using Microsoft.Identity.Client;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CourtCheckInPrism.Views
{
    public partial class LogIn : ContentPage
    {
        

        public LogIn()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            try
            {
                // Look for existing account
                IEnumerable<IAccount> accounts = await App.AuthenticationClient.GetAccountsAsync();

                AuthenticationResult result = await App.AuthenticationClient
                    .AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();

                await Navigation.PushAsync(new MasterPage(result));
            }
            catch
            {
                // Do nothing - the user isn't logged in
            }
            base.OnAppearing();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            AuthenticationResult result;
            try
            {
                result = await App.AuthenticationClient
                    .AcquireTokenInteractive(Constants.Scopes)
                    .WithPrompt(Prompt.SelectAccount)
                    .WithParentActivityOrWindow(App.UIParent)
                    .ExecuteAsync();

                await Navigation.PushAsync(new MasterPage(result));
            }
            catch (MsalException ex)
            {
                if (ex.Message != null && ex.Message.Contains("AADB2C90118"))
                {
                    result = await OnForgotPassword();
                    await Navigation.PushAsync(new MasterPage(result));
                }
                else if (ex.ErrorCode != "authentication_canceled")
                {
                    await DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "Dismiss");
                }
            }

        }
        async Task<AuthenticationResult> OnForgotPassword()
        {
            try
            {
                return await App.AuthenticationClient
                    .AcquireTokenInteractive(Constants.Scopes)
                    .WithPrompt(Prompt.SelectAccount)
                    .WithParentActivityOrWindow(App.UIParent)
                    .WithB2CAuthority(Constants.AuthorityPasswordReset)
                    .ExecuteAsync();
            }
            catch (MsalException)
            {
                // Do nothing - ErrorCode will be displayed in OnLoginButtonClicked
                return null;
            }
        }
    }
}