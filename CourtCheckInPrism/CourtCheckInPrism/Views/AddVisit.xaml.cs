using SQLite;
using Xamarin.Forms;

namespace CourtCheckInPrism.Views
{
    public partial class AddVisit : ContentPage
    {
        private SQLiteConnection conn;
        private CourtScheduleModel courtScheduleModel;
        public AddVisit()
        {
            InitializeComponent();
            //Getting database connection
            conn = DependencyService.Get<SQLiteInterface>().GetConnectionWithDatabase();
            conn.CreateTable<CourtScheduleModel>();
        }

        private void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            courtScheduleModel = new CourtScheduleModel();
            courtScheduleModel.OccurenceNo = OccurenceNo.Text;
            courtScheduleModel.NameOfAccused = NameOfAccused.Text;
            courtScheduleModel.DateOfCourtAppearence = DateOfCourtAppearence.Date;
            courtScheduleModel.CourtAppearenceTime = _timePicker.Time.ToString();
            courtScheduleModel.DateOfOffence = DateOfOffence.Date.ToString("dd MMMM YYYY");
            courtScheduleModel.CourtHouseAddress = Address.SelectedItem.ToString();
            courtScheduleModel.ReasonForAppearence = ReasonForAppearence.SelectedItem.ToString();
            conn.Insert(courtScheduleModel);
            
        }
    }
}