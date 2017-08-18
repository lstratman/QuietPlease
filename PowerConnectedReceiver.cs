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
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new [] { Intent.ActionPowerConnected })]
    public class PowerConnectedReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Synchronize(context);
        }

        public static void Synchronize(Context context)
        {
            using (AudioManager audioManager = (AudioManager)context.GetSystemService(Context.AudioService))
            {
                RingAction currentRingAction = audioManager.RingerMode.ToRingAction();

                if (currentRingAction == Settings.PowerConnectedAction)
                {
                    return;
                }

                audioManager.RingerMode = Settings.PowerConnectedAction.ToRingerMode();

                if (Settings.ShowNotifications)
                {
                    Toast.MakeText(context, String.Format(context.Resources.GetString(Resource.String.power_connected_notification), Settings.PowerConnectedAction.ToString("G").ToLower()), ToastLength.Short).Show();
                }
            }
        }
    }
}