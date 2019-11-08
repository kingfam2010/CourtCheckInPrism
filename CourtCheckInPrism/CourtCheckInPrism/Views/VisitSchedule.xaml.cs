using SQLite;
using System.Linq;
using Xamarin.Forms;

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
        }
    }
}