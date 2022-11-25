using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public static class UIManager
    {
        public static RectTransform canvas { get; private set; }
        public static RectTransform screensParent { get; private set; }
    
    
    
        public static void Init(RectTransform canvasRectTransform, RectTransform screensParentTransform)
        {
            canvas = canvasRectTransform;
            screensParent = screensParentTransform;
        }
    }
}

