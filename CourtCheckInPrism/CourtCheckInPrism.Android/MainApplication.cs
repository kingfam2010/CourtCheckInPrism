﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CourtCheckInPrism.Helper;
using Plugin.CurrentActivity;
using Shiny;

namespace CourtCheckInPrism.Droid
{
    [Application]
    public class MainApplication :ShinyAndroidApplication<BackgroundStartup>
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            //RegisterActivityLifecycleCallbacks(this);
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this);
            //A great place to initialize Xamarin.Insights and Dependency Services!
            Shiny.AndroidShinyHost.Init(this, new BackgroundStartup(), services => {
                // register any platform specific stuff you need here
            });
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            //UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}