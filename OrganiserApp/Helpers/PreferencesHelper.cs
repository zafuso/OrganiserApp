using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Helpers
{
    public class PreferencesHelper
    {
        private readonly IPreferences preferences;

        public PreferencesHelper(IPreferences preferences)
        {
            this.preferences = preferences;
        }

        public void SetString(string Key, string Value)
        {
            if (!String.IsNullOrEmpty(Key) && !String.IsNullOrEmpty(Value))
            preferences.Set(Key, Value);
        }

        public void SetBool(string Key, bool Value)
        {
            if (!String.IsNullOrEmpty(Key)) 
                preferences.Set(Key, Value);

        }

        public void Clear()
        {
            preferences.Clear();
        }

        public void Remove(string Key)
        {
            if (!String.IsNullOrEmpty(Key))
                preferences.Remove(Key);

        }

        public string GetString(string Key, string DefaultValue)
        {
            if (!String.IsNullOrEmpty(Key) && !String.IsNullOrEmpty(DefaultValue))
                return preferences.Get(Key, DefaultValue);

            return DefaultValue;
        }

        public bool GetBool(string Key, bool DefaultValue)
        {
            if (!String.IsNullOrEmpty(Key))
                preferences.Get(Key, DefaultValue);

            return DefaultValue;
        }
    }
}
