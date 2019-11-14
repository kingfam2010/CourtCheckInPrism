using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using CourtCheckInPrism.Infrastructure;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Syncfusion.ListView.XForms;
using Syncfusion.DataSource;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace CourtCheckInPrism.ViewModels
{
    public class VisitScheduleViewModel : AppMapViewModelBase, IActiveAware, System.ComponentModel.INotifyPropertyChanged
    {
        private SQLiteConnection conn;
        
        
        //static bool isAscending = true;
        //private Command<Object> locationTapCommand;
        //private Command<Object> dateTapCommand;

        //public Command<Object> LocationTapCommand
        //{
        //    get { return locationTapCommand; }
        //    set { SetProperty(ref locationTapCommand, value); }
        //}
        //public Command<Object> DateTapCommand
        //{
        //    get { return dateTapCommand; }
        //    set { SetProperty(ref dateTapCommand, value); }
        //}

        ////public ObservableCollection<CourtScheduleModel> courtScheduleInfo { get; set; }

#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }
        protected INavigationService _navigationService { get; }
        
        public VisitScheduleViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;        

            //LocationTapCommand = new Command<object>(LocationTapped);
            //DateTapCommand = new Command<object>(DateTapped);

        }
       
        //Method for sorting based on Court house Location 
        //private void LocationTapped(Object obj)
        //{
        //    var listView = obj as SfListView;

        //    listView.DataSource.GroupDescriptors.Clear();
        //    listView.DataSource.SortDescriptors.Clear();
        //    listView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
        //    {
        //        PropertyName = "CourtHouseAddress",
        //        KeySelector = (Object obj1) =>
        //        {
        //            return (obj1 as CourtScheduleModel).CourtHouseAddress;
        //        },
        //    });           
        //    listView.DataSource.SortDescriptors.Add(new SortDescriptor()
        //    {
        //        PropertyName = "DateOfCourtAppearence",
        //        Direction = isAscending ? ListSortDirection.Ascending : ListSortDirection.Descending
        //    });
        //    isAscending = isAscending ? false : true;
        //}

        //Method for sorting based on Date
        //private void DateTapped(Object obj)
        //{
        //    var listView = obj as SfListView;
        //    listView.DataSource.GroupDescriptors.Clear();
        //    listView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
        //    {
        //        PropertyName = "DateOfCourtAppearence",
        //        KeySelector = (Object obj1) =>
        //        {
        //            return (obj1 as CourtScheduleModel).DateOfCourtAppearence.ToShortDateString();
        //        },
        //    });
        //    listView.DataSource.SortDescriptors.Clear();
        //    listView.DataSource.SortDescriptors.Add(new SortDescriptor()
        //    {
        //        PropertyName = "DateOfCourtAppearence",
        //        Direction = isAscending ? ListSortDirection.Ascending : ListSortDirection.Descending
        //    });
        //    isAscending = isAscending ? false : true;
        //    listView.RefreshView();
        //}
    }
}
