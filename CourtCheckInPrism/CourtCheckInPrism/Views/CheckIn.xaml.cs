using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CourtCheckInPrism.Views
{
    public partial class CheckIn : ContentPage
    {
        public CheckIn(int id, string OccurenceNo, string CourtAppearenceTime, DateTime DateOfCourtAppearence, string NameOfAccused, string CourtHouseAddress )
        {
            InitializeComponent();
            IDEntry.Text = id.ToString();
            occNo.Text = OccurenceNo;
            nameOfAccused.Text = NameOfAccused;
            dateOfCourtApp.Text = DateOfCourtAppearence.ToString();
            courtAppearanceTime.Text = CourtAppearenceTime;
            location.Text = CourtHouseAddress;

        }
    }
}