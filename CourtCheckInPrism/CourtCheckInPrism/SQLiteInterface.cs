using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CourtCheckInPrism
{
    public interface SQLiteInterface
    {
        SQLiteConnection GetConnectionWithDatabase();
        SQLiteConnection GetConnectionWithDatabaseCreateTable();

        //Implementing CRUD functionality
        List<CourtScheduleModel> GetVisitSchedule();
        List<CourtScheduleModel> GetVisitHistory();
        bool UpdateCheckIn(CourtScheduleModel visit);
        bool UpdateCheckOut(CourtScheduleModel visit);
        void DeleteVisit(int Id);

        bool SaveVisit(CourtScheduleModel visit);

    }
}
