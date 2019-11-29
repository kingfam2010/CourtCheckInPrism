using SQLite;
using Xamarin.Forms;

namespace CourtCheckInPrism.Views
{
    public partial class VisitDetail : ContentPage
    {
        private readonly SQLiteConnection conn;
        private readonly CourtScheduleModel details;
        public VisitDetail(CourtScheduleModel details)
        {
            InitializeComponent();
            this.details = details;
            conn = DependencyService.Get<SQLiteInterface>().GetConnectionWithDatabase();
            IDEntry.Text = details.Id.ToString();
            occNo.Text = details.OccurenceNo;
            nameOfAccused.Text = details.NameOfAccused;
            dateOfCourtApp.Text = details.DateOfCourtAppearence.ToShortDateString();
            courtAppearanceTime.Text = details.CourtAppearenceTime;
            location.Text = details.CourtHouseAddress;
            checkIn.Text = details.CheckInTime.ToString();
            checkOut.Text = details.CheckOutTime.ToString();
            TestifyValue.Text = details.Testify;
            TimeCalledInValue.Text = details.TimeCalledIn;
            NoTestifyLabelValue.Text = details.NoTestifyReason;

        }
    }
}