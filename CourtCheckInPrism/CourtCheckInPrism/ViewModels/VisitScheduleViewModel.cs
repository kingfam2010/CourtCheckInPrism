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

        //IEnumerable<CourtScheduleModel> visitCollection;
        //public IEnumerable<CourtScheduleModel> VisitCollection
        //{
        //    get
        //    {
        //        if(visitCollection == null)
        //        {
        //            visitCollection = GetItems();
        //        }
        //        return visitCollection;
        //    }
        //}

        //public IEnumerable<CourtScheduleModel> GetItems()
        //{
        //    var table1 = (from i in conn.Table<CourtScheduleModel>() where i.CheckInTime == null select i);
        //    var table2 = (from i in conn.Table<CourtScheduleModel>() where i.CheckInTime != null && i.Testify == null select i);
        //    ObservableCollection<CourtScheduleModel> VisitList = new ObservableCollection<CourtScheduleModel>();
           
        //    if(table2.Count() == 0)
        //    {
        //        foreach (var visit in table2)
        //        {
        //            VisitList.Add(new CourtScheduleModel()
        //            {
        //                Id = visit.Id,
        //                CheckInTime = visit.CheckInTime,
        //                CheckOutTime = visit.CheckOutTime,
        //                CourtAppearenceTime = visit.CourtAppearenceTime,
        //                CourtHouseAddress = visit.CourtHouseAddress,
        //                DateOfCourtAppearence = visit.DateOfCourtAppearence,
        //                DateOfOffence = visit.DateOfOffence,
        //                LunchTimeEnd = visit.LunchTimeEnd,
        //                LunchTimeStart = visit.LunchTimeStart,
        //                NameOfAccused = visit.NameOfAccused,
        //                NoTestifyReason = visit.NoTestifyReason,
        //                OccurenceNo = visit.OccurenceNo,
        //                ReasonForAppearence = visit.ReasonForAppearence,
        //                Testify = visit.Testify,
        //                TimeCalledIn = visit.TimeCalledIn

        //            });
        //        }
                
        //    }
        //    else
        //    {
        //        foreach (var visit in table2)
        //        {
        //            VisitList.Add(new CourtScheduleModel()
        //            {
        //                Id = visit.Id,
        //                CheckInTime = visit.CheckInTime,
        //                CheckOutTime = visit.CheckOutTime,
        //                CourtAppearenceTime = visit.CourtAppearenceTime,
        //                CourtHouseAddress = visit.CourtHouseAddress,
        //                DateOfCourtAppearence = visit.DateOfCourtAppearence,
        //                DateOfOffence = visit.DateOfOffence,
        //                LunchTimeEnd = visit.LunchTimeEnd,
        //                LunchTimeStart = visit.LunchTimeStart,
        //                NameOfAccused = visit.NameOfAccused,
        //                NoTestifyReason = visit.NoTestifyReason,
        //                OccurenceNo = visit.OccurenceNo,
        //                ReasonForAppearence = visit.ReasonForAppearence,
        //                Testify = visit.Testify,
        //                TimeCalledIn = visit.TimeCalledIn

        //            });
        //        }
                
        //    }
        //    return VisitList;

        //}


#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }
        protected INavigationService _navigationService { get; }
        
        public VisitScheduleViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;
            conn = DependencyService.Get<SQLiteInterface>().GetConnectionWithDatabase();


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
