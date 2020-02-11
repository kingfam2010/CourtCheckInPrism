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
using CourtCheckInPrism.Helper;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace CourtCheckInPrism.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckIn : ContentPage
    {
        private SQLiteConnection conn;
        public DateTime checkInTime { get; set; }
        public DateTime checkOutTime { get; set; }
        public string geocodeAddress;
        private CourtScheduleModel details;
        public List<Position> CourtHouseCoordinates { get; set; }
        public Position selectedCourtLocation { get; set; }
        

        public CheckIn(CourtScheduleModel details)
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

            //Checking if Check in time already entered or not
            if(details.CheckInTime.ToString() == "0001-01-01 12:00:00 AM" || details.CheckInTime == null) {
                checkInLabel.IsVisible = false;
                checkIn.IsVisible = false;
                if(details.DateOfCourtAppearence.Date == DateTime.Today.Date)
                {
                    checkIn_Btn.IsVisible = true;
                }
                else
                {
                    checkIn_Btn.IsVisible = true;
                    checkIn_Btn.IsEnabled = false;
                    DisplayAlert("Alert!", $"Check-In only possible on {DateTime.Today.ToShortDateString()}", "OK");
                }                
                checkOut.IsVisible = false;
                checkOut_Btn.IsVisible = false;
                checkOutLabel.IsVisible = false;
                TestifyLabel.IsVisible = false;
                Testify.IsVisible = false;
                TimeCalledInLabel.IsVisible = false;
                TimeCalledIn.IsVisible = false;
                NoTestifyLabel.IsVisible = false;
                NoTestify.IsVisible = false;
                saveCheckOut_Btn.IsVisible = false;
                save_Btn.IsVisible = false;
                LunchStartLabel.IsVisible = false;
                LunchStart.IsVisible = false;
                LunchEndLabel.IsVisible = false;
                LunchEnd.IsVisible = false;

            }
            else
            {
                checkIn.Text = details.CheckInTime.ToString();
                checkInLabel.IsVisible = true;
                checkIn.IsVisible = true;
                checkIn_Btn.IsVisible = false;
                checkOut.IsVisible = false;
                checkOutLabel.IsVisible = false;
                checkOut_Btn.IsVisible = true;
                TestifyLabel.IsVisible = false;
                Testify.IsVisible = false;
                TimeCalledInLabel.IsVisible = false;
                TimeCalledIn.IsVisible = false;
                NoTestifyLabel.IsVisible = false;
                NoTestify.IsVisible = false;
                saveCheckOut_Btn.IsVisible = false;
                save_Btn.IsVisible = false;
                LunchStartLabel.IsVisible = false;
                LunchStart.IsVisible = false;
                LunchEndLabel.IsVisible = false;
                LunchEnd.IsVisible = false;
            }             

            //Court house co-ordinates list
            CourtHouseCoordinates = new List<Position>();
            CourtHouseCoordinates.Add(new Position(43.6605424, -79.7270547));
            CourtHouseCoordinates.Add(new Position(43.6577849, -79.7227285));
            CourtHouseCoordinates.Add(new Position(43.6602576, -79.7281912));

            if(location.Text.Equals("Davis Court"))
            {
                selectedCourtLocation = CourtHouseCoordinates[0];
                CustomMapAddress(selectedCourtLocation);

            }else if(location.Text.Equals("Ray Lawson"))
            {
                selectedCourtLocation = CourtHouseCoordinates[1];
                CustomMapAddress(selectedCourtLocation);
            }
            else if (location.Text.Equals("Mississauga"))
            {
                selectedCourtLocation = CourtHouseCoordinates[2];
                CustomMapAddress(selectedCourtLocation);
            }
            else
            {
                customMap.IsVisible = false;
            }                                       
        }

        private void CustomMapAddress(Position selectedCourtLocation)
        {
            var position = selectedCourtLocation;
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Court House"                
            };            
            customMap.Circle = new CustomCircle
            {
                Position = position,
                Radius = 200

            };
            customMap.Pins.Add(pin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(selectedCourtLocation, Distance.FromMiles(0.5)));
        }

        private async void checkIn_Btn_Clicked(object sender, EventArgs e)
        {
            //Getting permissions from user
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Going to need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    if (results.ContainsKey(Permission.Location))
                    {
                        status = results[Permission.Location];
                    }
                }

                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    IndicatorWebFetch.IsRunning = true;
                    checkIn_Btn.IsEnabled = false;
                    if (CrossGeolocator.Current.IsGeolocationAvailable)
                    {
                        if (CrossGeolocator.Current.IsGeolocationEnabled)
                        {
                            var locator = CrossGeolocator.Current;
                            locator.DesiredAccuracy = 100;

                            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                            //var position = await locator.GetLastKnownLocationAsync();
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
                                        checkIn.Text = checkInTime.ToString("dd MMMM yyyy HH:mm:ss");
                                        await DisplayAlert("Message", "You are at court house", "ok");
                                        save_Btn.IsVisible = true;
                                        
                                    }
                                    else
                                    {
                                        //checkOut_Btn.IsVisible = false;
                                        await DisplayAlert("Message", "You are not at court house, Please get to location and try again!!", "ok");
                                        await DisplayAlert("Message", "If you think there is issue with location services, please sign in at kiosk!!", "ok");
                                        checkIn_Btn.IsEnabled = true;
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
                                    await DisplayAlert("Error", ex.ToString(), "OK");
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
                    IndicatorWebFetch.IsRunning = false;
                }
                else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
                {
                    //location denied
                    await DisplayAlert("Location denied", "Cannot continue, try again", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
            
        }

        private bool IsPointInCircle(double radius, double latitude, double longitude)
        {
            double distance = Math.Sqrt(Math.Pow(selectedCourtLocation.Latitude - latitude, 2) + Math.Pow(selectedCourtLocation.Longitude - longitude, 2));
            return distance <= radius;
        }

        private async void save_Btn_Clicked(object sender, EventArgs e)
        {           
            details.CheckInTime = checkInTime;
            
            try 
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    conn.Update(details);
                });
                //string sql = $"UPDATE CourtScheduleModel SET CheckInTime='{details.CheckInTime}' WHERE Id={details.Id}";
                //conn.Execute(sql);
                await DisplayAlert("Message", " updated", "ok");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Message", "not updated", "ok");
            }
        }

        private void checkOut_Btn_Clicked(object sender, EventArgs e)
        {
            checkIn_Btn.IsVisible = false;
            checkOut.IsVisible = true;
            checkOutLabel.IsVisible = true;
            checkOut_Btn.IsVisible = false;
            checkOutTime = DateTime.Now;
            checkOut.Text = checkOutTime.ToString("dd MMMM yyyy HH: mm:ss");
            TestifyLabel.IsVisible = true;
            Testify.IsVisible = true;
            LunchOption.IsVisible = true;
            LunchOptionPick.IsVisible = true;
        }

        private async void saveCheckOut_Btn_Clicked(object sender, EventArgs e)
        {
            if (Testify.SelectedIndex == 0)
            {
                details.CheckOutTime = checkOutTime;
                details.Testify = Testify.SelectedItem.ToString();
                details.TimeCalledIn = TimeCalledIn.Time.ToString();
                details.LunchTimeStart = LunchStart.Time.ToString();
                details.LunchTimeEnd = LunchEnd.Time.ToString();
            }
            else
            {
                details.CheckOutTime = checkOutTime;
                details.Testify = Testify.SelectedItem.ToString();
                details.LunchTimeStart = LunchStart.Time.ToString();
                details.LunchTimeEnd = LunchEnd.Time.ToString();
                if (NoTestify.SelectedIndex == 11)
                {
                    details.NoTestifyReason = "Others:" + OtherReason.Text;
                }
                else
                {
                    details.NoTestifyReason = NoTestify.SelectedItem.ToString();
                }
            }
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    conn.Update(details);
                });
                
                await DisplayAlert("Message", " updated", "ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message.ToString(), "ok");
            }
        }

        private void Testify_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Testify.SelectedIndex == 0)
            {
                TimeCalledInLabel.IsVisible = true;
                TimeCalledIn.IsVisible = true;
                NoTestifyLabel.IsVisible = false;
                NoTestify.IsVisible = false;
                saveCheckOut_Btn.IsVisible = true;
            }
            else
            {
                TimeCalledInLabel.IsVisible = false;
                TimeCalledIn.IsVisible = false;
                NoTestifyLabel.IsVisible = true;
                NoTestify.IsVisible = true;
                saveCheckOut_Btn.IsVisible = true;
            }

        }

        private void NoTestify_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(NoTestify.SelectedIndex == 11)
            {
                OtherReason.IsVisible = true;
            }
        }

        private void LunchOptionPick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(LunchOptionPick.SelectedIndex == 0)
            {
                LunchStart.IsVisible = true;
                LunchStartLabel.IsVisible = true;
                LunchEnd.IsVisible = true;
                LunchEndLabel.IsVisible = true;
            }else if(LunchOptionPick.SelectedIndex == 1)
            {
                LunchStart.IsVisible = false;
                LunchStartLabel.IsVisible = false;
                LunchEnd.IsVisible = false;
                LunchEndLabel.IsVisible = false;
            }
        }

        
    }
}