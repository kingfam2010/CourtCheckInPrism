using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CourtCheckInPrism.Views;
using CourtCheckInPrism.ViewModels;

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
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            //await NavigationService.NavigateAsync("NavigationPage/AddVisit");
           
            await NavigationService.NavigateAsync("/MasterPage/NavigationPage/TabPage");
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
