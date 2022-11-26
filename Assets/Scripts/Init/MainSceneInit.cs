using Managers;
using UnityEngine;

namespace Init
{
    public class MainSceneInit : MonoBehaviour
    {
        [SerializeField] private RectTransform _canvasTransform;
    
        [SerializeField] private RectTransform _screensParent;

        public void Awake()
        {
            QualitySettings.SetQualityLevel(3, true);
            
            UIManager.Init(_canvasTransform, _screensParent);
        
            ScreensManager.OpenScreen(ScreenName.MainScreen, ScreenTransitionType.MoveUp);

            HeaderController.InitHeader();
        }
    }
}

