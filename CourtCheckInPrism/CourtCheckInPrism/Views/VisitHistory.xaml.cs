using SQLite;
using Syncfusion.DataSource;
using Xamarin.Forms;

namespace CourtCheckInPrism.Views
{
    public partial class VisitHistory : ContentPage
    {
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
            var details = e.ItemData as CourtScheduleModel;
            Navigation.PushAsync(new VisitDetail(details));
        }
    }
}