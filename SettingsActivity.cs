using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace QuietPlease
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@mipmap/icon")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Settings);

            FindViewById<RadioGroup>(Resource.Id.chargingRadioGroup).ClearCheck();

            switch (Settings.PowerConnectedAction)
            {
                case RingAction.Ring:
                    FindViewById<RadioButton>(Resource.Id.chargingRingRadioButton).Checked = true;
                    break;

                case RingAction.Silent:
                    FindViewById<RadioButton>(Resource.Id.chargingSilentRadioButton).Checked = true;
                    break;

                case RingAction.Vibrate:
                    FindViewById<RadioButton>(Resource.Id.chargingVibrateRadioButton).Checked = true;
                    break;
            }

            FindViewById<RadioGroup>(Resource.Id.notChargingRadioGroup).ClearCheck();

            switch (Settings.PowerDisconnectedAction)
            {
                case RingAction.Ring:
                    FindViewById<RadioButton>(Resource.Id.notChargingRingRadioButton).Checked = true;
                    break;

                case RingAction.Silent:
                    FindViewById<RadioButton>(Resource.Id.notChargingSilentRadioButton).Checked = true;
                    break;

                case RingAction.Vibrate:
                    FindViewById<RadioButton>(Resource.Id.notChargingVibrateRadioButton).Checked = true;
                    break;
            }

            FindViewById<CheckBox>(Resource.Id.showNotificationsCheckBox).Checked = Settings.ShowNotifications;

            FindViewById<RadioGroup>(Resource.Id.chargingRadioGroup).CheckedChange += Charging_CheckedChange;
            FindViewById<RadioGroup>(Resource.Id.notChargingRadioGroup).CheckedChange += NotCharging_CheckedChange;
            FindViewById<CheckBox>(Resource.Id.showNotificationsCheckBox).CheckedChange += ShowNotifications_CheckedChange;

            if (IsPowerConnected)
            {
                PowerConnectedReceiver.Synchronize(Application.Context);
            }

            else
            {
                PowerDisconnectedReceiver.Synchronize(Application.Context);
            }
        }

        private void ShowNotifications_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Settings.ShowNotifications = e.IsChecked;
        }

        private bool IsPowerConnected
        {
            get
            {
                using (IntentFilter filter = new IntentFilter(Intent.ActionBatteryChanged))
                {
                    using (Intent battery = Application.Context.RegisterReceiver(null, filter))
                    {
                        return battery.GetIntExtra(BatteryManager.ExtraPlugged, -1) != 0;
                    }
                }
            }
        }

        private void Charging_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            switch (e.CheckedId)
            {
                case Resource.Id.chargingRingRadioButton:
                    Settings.PowerConnectedAction = RingAction.Ring;
                    break;

                case Resource.Id.chargingVibrateRadioButton:
                    Settings.PowerConnectedAction = RingAction.Vibrate;
                    break;

                case Resource.Id.chargingSilentRadioButton:
                    Settings.PowerConnectedAction = RingAction.Silent;
                    break;
            }

            if (IsPowerConnected)
            {
                PowerConnectedReceiver.Synchronize(ApplicationContext);
            }
        }

        private void NotCharging_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            switch (e.CheckedId)
            {
                case Resource.Id.notChargingRingRadioButton:
                    Settings.PowerDisconnectedAction = RingAction.Ring;
                    break;

                case Resource.Id.notChargingVibrateRadioButton:
                    Settings.PowerDisconnectedAction = RingAction.Vibrate;
                    break;

                case Resource.Id.notChargingSilentRadioButton:
                    Settings.PowerDisconnectedAction = RingAction.Silent;
                    break;
            }

            if (!IsPowerConnected)
            {
                PowerDisconnectedReceiver.Synchronize(ApplicationContext);
            }
        }
    }
}

