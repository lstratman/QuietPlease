using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace QuietPlease
{
    public enum RingAction
    {
        Ring = 2,
        Vibrate = 1,
        Silent = 0
    }

    public static class RingerModeExtensions
    {
        public static RingAction ToRingAction(this RingerMode ringerMode)
        {
            return (RingAction)((int)ringerMode);
        }

        public static RingerMode ToRingerMode(this RingAction ringAction)
        {
            return (RingerMode)((int)ringAction);
        }
    }
}