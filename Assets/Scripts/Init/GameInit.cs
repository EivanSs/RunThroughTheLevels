using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Init
{
    public class GameInit : MonoBehaviour
    {
        public SettingsList settingsList;

        public void Awake()
        {
            settingsList.Init();
        
            WeaponsManager.Initialize();
            ArmorManager.Initialize();
            
            QualitySettings.SetQualityLevel(3, true);

            SceneManager.LoadScene(1);
        }
    }
}

