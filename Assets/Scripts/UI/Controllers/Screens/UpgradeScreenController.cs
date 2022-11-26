using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UI.Reusable;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Controllers.Screens
{
    public class UpgradeScreenController : BaseScreen
    {
        [SerializeField] private Button _buttonBack;

        public override bool enableInfoBar
        {
            get
            {
                return true;
            }
        }
        
        public void Start()
        {
            _buttonBack.onClick.AddListener(() =>
            {
                ScreensManager.OpenScreen(ScreenName.BoutiqueScreen, ScreenTransitionType.MoveDown);
                
                HeaderController.ShowHeader();
            });
            
            _infoBarController.UpdateInfo(this, InfoBarVariousType.Upgrade, o => {});
        }
    }
}
