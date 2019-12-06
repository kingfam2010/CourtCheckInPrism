using SQLite;
using System;
using System.Collections.ObjectModel;
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
            DateOfCourtAppearence.MinimumDate = DateTime.Today;
            DateOfOffence.MaximumDate = DateTime.Today;


        }

        private void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            //Adding data to Database
            try {
            courtScheduleModel = new CourtScheduleModel();
            courtScheduleModel.OccurenceNo = OccurenceNo.Text;
            courtScheduleModel.NameOfAccused = NameOfAccused.Text;
            courtScheduleModel.DateOfCourtAppearence = DateOfCourtAppearence.Date;
            courtScheduleModel.CourtAppearenceTime = _timePicker.Time.ToString();
            courtScheduleModel.DateOfOffence = DateOfOffence.Date.ToString("dd MMMM YYYY");
            courtScheduleModel.CourtHouseAddress = Address.SelectedItem.ToString();
            courtScheduleModel.ReasonForAppearence = ReasonForAppearence.SelectedItem.ToString();
            courtScheduleModel.CheckInTime = null;
            courtScheduleModel.CheckOutTime = null;
            conn.Insert(courtScheduleModel);
            DisplayAlert("Message", "Visit Saved", "OK");
            //Navigation.PopAsync();
            }
            catch(Exception ex)
            {
                DisplayAlert("Message", "Please Enter all the fields!!!", "OK");
                
            }
        }
    }
}