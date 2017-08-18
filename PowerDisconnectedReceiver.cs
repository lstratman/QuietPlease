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
    [IntentFilter(new[] { Intent.ActionPowerDisconnected })]
    public class PowerDisconnectedReceiver : BroadcastReceiver
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

                if (currentRingAction == Settings.PowerDisconnectedAction)
                {
                    return;
                }

                audioManager.RingerMode = Settings.PowerDisconnectedAction.ToRingerMode();

                if (Settings.ShowNotifications)
                {
                    Toast.MakeText(context, String.Format(context.Resources.GetString(Resource.String.power_disconnected_notification), Settings.PowerDisconnectedAction.ToString("G").ToLower()), ToastLength.Short).Show();
                }
            }
        }
    }
}