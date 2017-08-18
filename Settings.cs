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

namespace QuietPlease
{
    public class Settings
    {
        private static RingAction _PowerConnectedAction;
        private static RingAction _PowerDisconnectedAction;
        private static bool _ShowNotifications;
        private static ISharedPreferences _Preferences;

        static Settings()
        {
            _Preferences = Application.Context.GetSharedPreferences("QuietPlease", FileCreationMode.Private);

            _PowerConnectedAction = (RingAction)_Preferences.GetInt("PowerConnectedAction", (int) RingAction.Ring);
            _PowerDisconnectedAction = (RingAction)_Preferences.GetInt("PowerDisconnectedAction", (int)RingAction.Vibrate);
            _ShowNotifications = _Preferences.GetBoolean("ShowNotifications", true);
        }

        public static RingAction PowerConnectedAction
        {
            get
            {
                return _PowerConnectedAction;
            }

            set
            {
                _PowerConnectedAction = value;

                ISharedPreferencesEditor preferencesEditor = _Preferences.Edit();

                preferencesEditor.PutInt("PowerConnectedAction", (int)value);
                preferencesEditor.Commit();
            }
        }

        public static RingAction PowerDisconnectedAction
        {
            get
            {
                return _PowerDisconnectedAction;
            }

            set
            {
                _PowerDisconnectedAction = value;

                ISharedPreferencesEditor preferencesEditor = _Preferences.Edit();

                preferencesEditor.PutInt("PowerDisconnectedAction", (int)value);
                preferencesEditor.Commit();
            }
        }

        public static bool ShowNotifications
        {
            get
            {
                return _ShowNotifications;
            }

            set
            {
                _ShowNotifications = value;

                ISharedPreferencesEditor preferencesEditor = _Preferences.Edit();

                preferencesEditor.PutBoolean("ShowNotifications", value);
                preferencesEditor.Commit();
            }
        }
    }
}