using System;
using SQLite;
using Syncfusion.DataSource;
using Xamarin.Forms;

namespace CourtCheckInPrism.Views
{
    public partial class VisitHistory : ContentPage
    {
        SearchBar searchBar = null;
        private SQLiteConnection conn;
        public VisitHistory()
        {
            InitializeComponent();
            conn = DependencyService.Get<SQLiteInterface>().GetConnectionWithDatabase();
            var schedule = (from sch in conn.Table<CourtScheduleModel>() where sch.Testify != null select sch);
            historyListView.ItemsSource = schedule;

            historyListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "DateOfCourtAppearence",
                KeySelector = (object obj) =>
                {
                    var item = (obj as CourtScheduleModel);
                    return (item.DateOfCourtAppearence.ToShortDateString());
                },
            });
        }

        private void historyListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if(e.ItemType != Syncfusion.ListView.XForms.ItemType.GroupHeader)
            {
                var details = e.ItemData as CourtScheduleModel;
                Navigation.PushAsync(new VisitDetail(details));
            }
            else
            {
                return;
            }
            
        }

        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (historyListView.DataSource != null)
            {
                this.historyListView.DataSource.Filter = FilterLocation;
                this.historyListView.DataSource.RefreshFilter();
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