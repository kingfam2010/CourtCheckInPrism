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
        public SQLiteConnection GetConnectionWithDatabase()
        {
            //throw new NotImplementedException();
            var dbName = "SampleDatabase.db1";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, dbName);

            SQLiteConnection database = new SQLiteConnection(path);

            database.CreateTable<CourtScheduleModel>();

            return database;
        }
    }
}