using System;

namespace Utils
{
    public static class SpareUtils
    {
        public static bool IsNullOrDestroyed(this System.Object obj) 
        {
            if (ReferenceEquals(obj, null)) return true;
 
            if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;
 
            return false;
        }
    }
}