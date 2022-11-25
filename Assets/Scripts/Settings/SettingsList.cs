using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/SettingsList")]
    public class SettingsList : ScriptableObject
    {
        public List<ScriptableObject> settings;
    
        private static List<ScriptableObject> _settings;

        public void Init()
        {
            _settings = settings;
        }

        public static T Get<T>() where T : ScriptableObject
        {
            if (_settings == null)
                throw new Exception("Settings is not loaded");
        
            ScriptableObject scriptableObject = _settings.FirstOrDefault(v => v is T);

            if (scriptableObject != null && scriptableObject is T result)
                return result;
        
            throw new Exception("Can`t find settings");
        }
    }
}