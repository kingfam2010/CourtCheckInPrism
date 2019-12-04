using System;
using System.Collections.Generic;
using System.Text;

//namespace CourtCheckInPrism.Helper
//{
//    public class CrossGeofenceListener: IGeofenceListener
//    {
//        public void OnMonitoringStarted(string region)
//        {
//            System.Diagnostics.Debug.WriteLine(string.Format("{0} - Monitoring started in region: {1}", CrossGeofence.Tag, region));
//        }

//        public void OnMonitoringStopped()
//        {
//            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Tag, "Monitoring stopped for all regions"));
//        }

//        public void OnMonitoringStopped(string identifier)
//        {
//            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2}", CrossGeofence.Tag, "Monitoring stopped in region", identifier));
//        }

//        public void OnError(string error)
//        {
//            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2}", CrossGeofence.Tag, "Error", error));
//        }

//        // Note that you must call CrossGeofence.GeofenceListener.OnAppStarted() from your app when you want this method to run.
//        public void OnAppStarted()
//        {
//            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Tag, "App started"));
//        }

//        public void OnRegionStateChanged(GeofenceResult result)
//        {
//            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Tag, result.ToString()));
//        }
//    }
//}

