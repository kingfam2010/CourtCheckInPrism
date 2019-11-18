using SQLite;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using System.Collections.Generic;

namespace CourtCheckInPrism.Views
{
    public partial class VisitSchedule : ContentPage
    {
        
        private SQLiteConnection conn;
        public VisitSchedule()
        {
            InitializeComponent();
            PopulateVisitSchedule();


            //listView.GroupHeaderTemplate = new DataTemplate(() =>
            //{
            //   var grid = new Grid();
            //    var headerLabel = new Label
            //    {
            //        TextColor = Color.White,
            //        FontAttributes = FontAttributes.Bold,
            //        BackgroundColor = Color.Teal
            //    };
            //    headerLabel.SetBinding(Label.TextProperty, new Binding("key"));
            //    grid.Children.Add(headerLabel);
            //    return grid;
            //});

            //listView.DataSource.SortDescriptors.Add(new SortDescriptor()
            //{
            //    PropertyName = "CourtAppearenceTime",
            //    Direction = ListSortDirection.Ascending
            //});



        }
        //protected override void OnAppearing()
        //{
        //    PopulateVisitSchedule();
        //}

        public void PopulateVisitSchedule()
        {
           
            //listView.ItemsSource = null;
            List<CourtScheduleModel> scheduleList = DependencyService.Get<SQLiteInterface>().GetVisitSchedule();
            var schedule = (from sch in scheduleList select sch);
            listView.ItemsSource = schedule;
            //listView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            //{
            //    PropertyName = "DateOfCourtAppearence",
            //    KeySelector = (object obj) =>
            //    {
            //        var item = (obj as CourtScheduleModel);
            //        return (item.DateOfCourtAppearence.ToShortDateString());
            //    },
            //});
            //var schedule = (from sch in conn.Table<CourtScheduleModel>() select sch);
            //listView.ItemsSource = schedule;
        }

        public void Destroy()
        {
            listView.Behaviors.Clear();
        }

        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //Navigation.PushAsync(new CheckIn());
            var details = e.ItemData as CourtScheduleModel;
            Navigation.PushAsync(new CheckIn(details));
        }
    }
}