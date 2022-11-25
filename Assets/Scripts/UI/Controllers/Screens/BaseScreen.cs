using System;
using System.Collections;
using System.Collections.Generic;
using UI.Reusable;
using UnityEngine;

namespace UI.Controllers.Screens
{
    public class BaseScreen : MonoBehaviour
    {
        protected InfoBarController _infoBarController;

        public virtual bool enableInfoBar
        {
            get
            {
                return false;
            }
        }

        public void SetupInfoBar(InfoBarController infoBarController)
        {
            _infoBarController = infoBarController;
        }
    }
}

