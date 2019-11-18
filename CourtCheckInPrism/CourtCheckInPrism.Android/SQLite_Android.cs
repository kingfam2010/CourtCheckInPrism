using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CourtCheckInPrism.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace CourtCheckInPrism.Droid
{
    public class SQLite_Android : SQLiteInterface
    {
        SQLiteConnection database;
        public void DeleteVisit(int Id)
        {
            
            string sql = $"DELETE FROM CourtScheduleModel WHERE Id={Id}";
            database.Execute(sql);
        }

        public SQLiteConnection GetConnectionWithDatabase()
        {
            //throw new NotImplementedException();
            var dbName = "SampleDatabase.db2";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, dbName);

            database = new SQLiteConnection(path);

            

            return database;
        }

        public List<CourtScheduleModel> GetVisitHistory()
        {
            
            string sql = "SELECT * FROM CourtScheduleModel WHERE CheckOutTime IS NOT NULL";
            List<CourtScheduleModel> visits = database.Query<CourtScheduleModel>(sql);
            return visits;
        }

        public List<CourtScheduleModel> GetVisitSchedule()
        {
            
            string sql = "SELECT * FROM CourtScheduleModel WHERE CheckOutTime IS NULL";
            List<CourtScheduleModel> visits = database.Query<CourtScheduleModel>(sql);
            return visits;
        }

        public bool UpdateCheckOut(CourtScheduleModel visit)
        {
            
            bool res = false;
            try
            {
                string sql = $"UPDATE CourtScheduleModel SET CheckInTime='{visit.CheckInTime}', CheckOutTime='{visit.CheckOutTime}', Testify='{visit.Testify}', TimeCalledIn='{visit.TimeCalledIn}', NoTestifyReason='{visit.NoTestifyReason}' WHERE Id={visit.Id}";
                database.Execute(sql);
                res = true;
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public bool UpdateCheckIn(CourtScheduleModel visit)
        {
            
            bool res = false;
            try
            {
                string sql = $"UPDATE CourtScheduleModel SET CheckInTime='{visit.CheckInTime}' WHERE Id={visit.Id}";
                database.Execute(sql);
                res = true;
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public bool SaveVisit(CourtScheduleModel visit)
        {
            
            bool res = false;
            try
            {
                database.Insert(visit);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        public SQLiteConnection GetConnectionWithDatabaseCreateTable()
        {
            var dbName = "SampleDatabase.db2";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, dbName);

            database = new SQLiteConnection(path);

            database.CreateTable<CourtScheduleModel>();

            return database;
        }
    }
}