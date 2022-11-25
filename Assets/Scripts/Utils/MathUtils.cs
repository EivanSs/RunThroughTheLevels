using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class MathUtils 
    {
        public static float Get0Or1(this float value)
        {
            if (value > 0)
                return 1;
        
            if (value < 0)
                return -1;

            return 0;
        }
    }
}

