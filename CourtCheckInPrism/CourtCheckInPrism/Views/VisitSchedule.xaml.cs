using SQLite;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;

namespace CourtCheckInPrism.Views
{
    public partial class VisitSchedule : ContentPage
    {
        
        private SQLiteConnection conn;
        public VisitSchedule()
        {
            InitializeComponent();
            conn = DependencyService.Get<SQLiteInterface>().GetConnectionWithDatabase();
            var schedule = (from sch in conn.Table<CourtScheduleModel>() select sch);
            listView.ItemsSource = schedule;
            //listView.GroupHeaderTemplate = new DataTemplate(() =>
            //{
            //   var grid = new Grid();
            //    var headerLabel = new Label
            //    {
            //        TextColor = Color.White,
            //        FontAttributes = FontAttributes.Bold,
            //        BackgroundColor = Color.Teal
            //    };
            //    headerLabel.SetBinding(Label.TextProperty, new Binding("key"));
            //    grid.Children.Add(headerLabel);
            //    return grid;
            //});

            //listView.DataSource.SortDescriptors.Add(new SortDescriptor()
            //{
            //    PropertyName = "CourtAppearenceTime",
            //    Direction = ListSortDirection.Ascending
            //});
            listView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "DateOfCourtAppearence",
                KeySelector = (object obj) =>
                {
                    var item = (obj as CourtScheduleModel);
                    return (item.DateOfCourtAppearence.ToShortDateString());
                },
            });
          
            
        }

        public void Destroy()
        {
            listView.Behaviors.Clear();
        }

        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //Navigation.PushAsync(new CheckIn());
            var details = e.ItemData as CourtScheduleModel;
            Navigation.PushAsync(new CheckIn(details.Id, details.OccurenceNo, details.CourtAppearenceTime, details.DateOfCourtAppearence, details.NameOfAccused, details.CourtHouseAddress));
        }
    }
}