using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CourtCheckInPrism.Views;
using CourtCheckInPrism.ViewModels;
using Microsoft.Identity.Client;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CourtCheckInPrism
{
    public partial class App

    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public static IPublicClientApplication AuthenticationClient { get; private set; }
        public static object UIParent { get; set; } = null;
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjA1NTUzQDMxMzcyZTM0MmUzMER1SEFVZ2dSaldZUlRCcFdYeEtUL3R1SWlaY2RhNGhvY1QxVEJ6d2NWZVU9");
            InitializeComponent();
            //await NavigationService.NavigateAsync("/MasterPage/NavigationPage/TabPage");
            await NavigationService.NavigateAsync("LogIn");

            //azure client id setup for b2c database
            AuthenticationClient = PublicClientApplicationBuilder.Create(Constants.ClientId)
            .WithIosKeychainSecurityGroup(Constants.IosKeychainSecurityGroups)
            .WithB2CAuthority(Constants.AuthoritySignin)
            .WithRedirectUri($"msal{Constants.ClientId}://auth")
            .Build();

            //await NavigationService.NavigateAsync("NavigationPage/AddVisit");           
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LogIn, LogInViewModel>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>("MasterPage");
            containerRegistry.RegisterForNavigation<TabPage, TabPageViewModel>();
            containerRegistry.RegisterForNavigation<VisitSchedule, VisitScheduleViewModel>();
            containerRegistry.RegisterForNavigation<VisitHistory, VisitHistoryViewModel>();
            containerRegistry.RegisterForNavigation<AddVisit, AddVisitViewModel>();
            containerRegistry.RegisterForNavigation<CheckIn, CheckInViewModel>();
            containerRegistry.RegisterForNavigation<VisitDetail, VisitDetailViewModel>();
        }
        protected override void OnResume()
        {
            base.OnResume();

            // TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.

        }

        protected override void OnSleep()
        {
            base.OnSleep();

            // TODO: This is the time to save app data in case the process is terminated.
            // This is the perfect timing to release exclusive resources (camera, I/O devices, etc...)

        }
    }
}
