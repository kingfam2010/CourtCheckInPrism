using SQLite;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourtCheckInPrism.Views
{
    public partial class VisitSchedule : ContentPage
    {
        SearchBar searchBar = null;
        private readonly SQLiteConnection conn;
        //public IEnumerable<CourtScheduleModel> CourtScheduleCollection
        //{
        //    get
        //    {
        //        if (CourtScheduleCollection == null)
        //            CourtScheduleCollection = GetItems();
        //        return CourtScheduleCollection;
        //    }
        //    set { }
        //}

        //private IEnumerable<CourtScheduleModel> GetItems()
        //{
        //    var schedule = (from sch in conn.Table<CourtScheduleModel>() where sch.Testify == null select sch);
        //    ObservableCollection<CourtScheduleModel> ScheduleList = new ObservableCollection<CourtScheduleModel>();
        //    foreach(var order in schedule)
        //    {
        //        ScheduleList.Add(new CourtScheduleModel()
        //        {
        //            Id = order.Id,
        //            OccurenceNo = order.OccurenceNo,
        //            NameOfAccused = order.NameOfAccused,
        //            DateOfCourtAppearence = order.DateOfCourtAppearence,
        //            CourtAppearenceTime = order.CourtAppearenceTime,
        //            DateOfOffence = order.DateOfOffence,
        //            CourtHouseAddress = order.CourtHouseAddress,
        //            ReasonForAppearence = order.ReasonForAppearence,
        //            CheckInTime = order.CheckInTime,
        //            CheckOutTime = order.CheckOutTime,
        //            Testify = order.Testify,
        //            TimeCalledIn = order.TimeCalledIn,
        //            NoTestifyReason = order.NoTestifyReason
        //        }); 
        //    }
        //    return ScheduleList;
        //}

        public VisitSchedule()
        {
            MessagingCenter.Unsubscribe<string>(this, "service2");
            MessagingCenter.Subscribe<string>(this, "service2", (value) =>
            {
                if (value == "1")
                {
                    notify1();
                }
                else
                {
                    notify2();
                }
            });
            InitializeComponent();
            conn = DependencyService.Get<SQLiteInterface>().GetConnectionWithDatabase();
            //var schedule = (from sch in conn.Table<CourtScheduleModel>() where sch.CheckOutTime == null select sch);
            //listView.ItemsSource = null;
            //listView.ItemsSource = schedule;            
            var schedule1 = (from sch in conn.Table<CourtScheduleModel>() where sch.CheckInTime == null select sch);
            var schedule2 = (from sch in conn.Table<CourtScheduleModel>() where sch.CheckInTime != null && sch.Testify == null select sch);
            if (schedule2.Count() == 0) {
                listView.ItemsSource = null;
                listView.ItemsSource = schedule1;
            }
            else 
            {
                listView.ItemsSource = null;
                listView.ItemsSource = schedule2;
            }
           
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

        private async void notify2()
        {
            await DisplayAlert("Message", "You are not at court house", "ok");
        }

        private async void notify1()
        {
            await DisplayAlert("Message", "You are at court house", "ok");
        }

        private void populate()
        {
            throw new NotImplementedException();
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