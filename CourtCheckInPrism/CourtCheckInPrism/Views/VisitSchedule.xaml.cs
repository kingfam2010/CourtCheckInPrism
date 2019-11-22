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
        SearchBar searchBar = null;
        private SQLiteConnection conn;
        public VisitSchedule()
        {
            InitializeComponent();
            conn = DependencyService.Get<SQLiteInterface>().GetConnectionWithDatabase();            
            var schedule = (from sch in conn.Table<CourtScheduleModel>() where sch.Testify == null select sch);
            listView.ItemsSource = schedule;
            //var schedule1 = (from sch in conn.Table<CourtScheduleModel>() where (sch.CheckInTime == null AND sch.CheckOutTime == null) select sch);
            //var schedule2 = (from sch in conn.Table<CourtScheduleModel>() where sch.CheckInTime != null && sch.CheckOutTime == null select sch);
            //if (schedule2 != null) {
            //    listView.ItemsSource = schedule2;
            //}
            //else
            //{
            //    listView.ItemsSource = schedule1;
            //}

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
            if (e.ItemType != Syncfusion.ListView.XForms.ItemType.GroupHeader)
            {
                var details = e.ItemData as CourtScheduleModel;
                Navigation.PushAsync(new CheckIn(details));
            }
            else
            {
                return;
            }
                
            
        }

        private void filterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (listView.DataSource != null)
            {
                this.listView.DataSource.Filter = FilterLocation;
                this.listView.DataSource.RefreshFilter();
            }
        }

        private bool FilterLocation(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var visits = obj as CourtScheduleModel;
            if (visits.CourtHouseAddress.ToLower().Contains(searchBar.Text.ToLower())
                 || visits.CourtHouseAddress.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}