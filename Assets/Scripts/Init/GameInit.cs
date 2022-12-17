using Managers;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Init
{
    public class GameInit : MonoBehaviour
    {
        [SerializeField] private SettingsList _settingsList;

        public void Awake()
        {
            _settingsList.Init();
        
            WeaponsManager.Initialize();
            ArmorManager.Initialize();
            
            QualitySettings.SetQualityLevel(3, true);

            SceneManager.LoadScene(2);
        }
    }
}

