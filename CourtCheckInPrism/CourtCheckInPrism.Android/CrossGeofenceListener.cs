using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Geofence;
using Plugin.Geofence.Abstractions;
using Xamarin.Forms.Internals;

namespace CourtCheckInPrism.Droid
{
    public class CrossGeofenceListener: IGeofenceListener
    {
        public void OnMonitoringStarted(string region)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - Monitoring started in region: {1}", CrossGeofence.Id, region));
        }

        public void OnMonitoringStopped()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, "Monitoring stopped for all regions"));
        }

        public void OnMonitoringStopped(string identifier)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2}", CrossGeofence.Id, "Monitoring stopped in region", identifier));
        }

        public void OnError(string error)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2}", CrossGeofence.Id, "Error", error));
        }

        // Note that you must call CrossGeofence.GeofenceListener.OnAppStarted() from your app when you want this method to run.
        public void OnAppStarted()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, "App started"));
        }

        public void OnRegionStateChanged(GeofenceResult result)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, result.ToString()));
        }

        public void OnLocationChanged(GeofenceLocation location)
        {
            throw new NotImplementedException();
        }
    }
}