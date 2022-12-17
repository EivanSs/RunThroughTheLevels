using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class MathUtils 
    {
        public static double DegToRad = Math.PI / 180;

        
        public static float Get0Or1(this float value)
        {
            if (value > 0)
                return 1;
        
            if (value < 0)
                return -1;

            return 0;
        }
        
        public static void SpreadVectors(UniqueList<FloatVector> points, int iterations, float matchDistance, float caf, int boundsMin, int boundsMax)
        {
            for (int i = 0; i < iterations; i++)
            {
                foreach (var p in points)
                {
                    foreach (var p2 in points)
                    {
                        var distance = (float)p.Distance(p2);
                        
                        if (distance > matchDistance)
                            continue;

                        distance -= matchDistance;

                        var angle = Math.Atan2(p.y - p2.y, p.x - p2.x);

                        p.x -= (float)Math.Cos(angle) * distance * caf;
                        p.y -= (float)Math.Sin(angle) * distance * caf;
                        
                        p2.x -= (float)Math.Cos(angle + Math.PI) * distance * caf;
                        p2.y -= (float)Math.Sin(angle + Math.PI) * distance * caf;
                    }
                }
            }

            UniqueList<FloatVector> outOfBounds = new UniqueList<FloatVector>();

            foreach (var point in points)
            {
                if (point.x < boundsMin || point.x > boundsMax || point.y < boundsMin || point.y > boundsMax)
                    outOfBounds.Add(point);
            }
            
            points.RemoveElements(outOfBounds);
        }
    }
}

