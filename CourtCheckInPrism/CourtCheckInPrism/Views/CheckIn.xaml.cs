using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Geolocator;
using SQLite;

namespace CourtCheckInPrism.Views
{
    public partial class CheckIn : ContentPage
    {
        public string geocodeAddress;
        CourtScheduleModel visitDetails;
        public List<Position> CourtHouseCoordinates { get; set; }
        //private SQLiteConnection conn;
        public Position selectedCourtLocation { get; set; }
        public DateTime checkInTime { get; set; }
        public CheckIn(CourtScheduleModel details)
        {
            InitializeComponent();

            if (details != null) {
                visitDetails = details;
                PopulateDetails(visitDetails);

            }
            
            
            //Connecting to database
            //conn = DependencyService.Get<SQLiteInterface>().GetConnectionWithDatabase();
            //conn.CreateTable<CourtScheduleModel>();

            CourtHouseCoordinates = new List<Position>();
            CourtHouseCoordinates.Add(new Position(43.6605424, -79.7270547));
            CourtHouseCoordinates.Add(new Position(43.6577849, -79.7227285));
            CourtHouseCoordinates.Add(new Position(43.6602576, -79.7281912));

            if(location.Text.Equals("Davis Court"))
            {
                selectedCourtLocation = CourtHouseCoordinates[0];

            }else if(location.Text.Equals("Ray Lawson"))
            {
                selectedCourtLocation = CourtHouseCoordinates[1];
            }else if (location.Text.Equals("Mississauga"))
            {
                selectedCourtLocation = CourtHouseCoordinates[1];
            }
            checkInLabel.IsVisible = false;
            checkOutLabel.IsVisible = false;
            checkIn.IsVisible = false;
            checkOut.IsVisible = false;

        }

        private void PopulateDetails(CourtScheduleModel details)
        {
            IDEntry.Text = details.Id.ToString();
            occNo.Text = details.OccurenceNo;
            nameOfAccused.Text = details.NameOfAccused;
            dateOfCourtApp.Text = details.DateOfCourtAppearence.ToShortDateString();
            courtAppearanceTime.Text = details.CourtAppearenceTime;
            location.Text = details.CourtHouseAddress;
            if(details.CheckInTime != null)
            {
                checkIn.Text = details.CheckInTime.ToString();
                checkInLabel.IsVisible = true;
                checkIn.IsVisible = true;
                checkIn_Btn.IsVisible = false;
            }
            else
            {
                checkInLabel.IsVisible = false;
                checkIn.IsVisible = false;
                checkIn_Btn.IsVisible = true;
            }
        }

        private async void checkIn_Btn_Clicked(object sender, EventArgs e)
        {
            if (CrossGeolocator.Current.IsGeolocationAvailable)
            {
                if (CrossGeolocator.Current.IsGeolocationEnabled)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;

                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));


                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            var latitude = position.Latitude;
                            var longitude = position.Longitude;

                            Console.WriteLine(latitude);
                            Console.WriteLine(longitude);                           

                            //Checking if point in circle
                            if (IsPointInCircle(0.03, latitude, longitude))
                            {
                                checkIn_Btn.IsVisible = false;
                                checkInLabel.IsVisible = true;
                                checkIn.IsVisible = true;
                                checkInTime = DateTime.Now;
                                checkIn.Text = checkInTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                                UpdateDatabase();
                                await DisplayAlert("Message", "You are at court house", "ok");

                            }
                            else
                            {
                                //checkOut_Btn.IsVisible = false;
                                await DisplayAlert("Message", "You are not at court house", "ok");

                            }


                            //var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);

                            //var placemark = placemarks?.FirstOrDefault();
                            //if (placemark != null)
                            //{
                            //    geocodeAddress =
                            //        $"Province:       {placemark.AdminArea}\n" +
                            //        $"CountryCode:     {placemark.CountryCode}\n" +
                            //        //$"CountryName:     {placemark.CountryName}\n" +
                            //        $"FeatureName:     {placemark.FeatureName}\n" +
                            //        $"Locality:        {placemark.Locality}\n" +
                            //        $"PostalCode:      {placemark.PostalCode}\n" +
                            //        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                            //        //$"SubLocality:     {placemark.SubLocality}\n" +
                            //        $"StreetNumber: {placemark.SubThoroughfare}\n" +
                            //        $"Street:    {placemark.Thoroughfare}\n";

                            //    Console.WriteLine(geocodeAddress);
                            //    //address.Text = geocodeAddress;

                                
                            //}

                        }
                        catch (Exception ex)
                        {
                            _ = ex.Message;
                        }

                    });
                }
                else
                {
                    await DisplayAlert("Message", "GPS not enabled", "ok");
                }
            }
            else
            {
                await DisplayAlert("Message", "GPS not available", "ok");

            }
        }

        private void UpdateDatabase()
        {
            visitDetails.CheckInTime = checkInTime;
            bool res = DependencyService.Get<SQLiteInterface>().UpdateCheckIn(visitDetails);
            
        }

        private bool IsPointInCircle(double radius, double latitude, double longitude)
        {
            double distance = Math.Sqrt(Math.Pow(selectedCourtLocation.Latitude - latitude, 2) + Math.Pow(selectedCourtLocation.Longitude - longitude, 2));
            return distance <= radius;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}