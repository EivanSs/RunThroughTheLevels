using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class AllUtils
    {
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
        
        public static float Degree(float value, float step, float iterations)
        {
            for (int i = 0; i < iterations; i++)
                value *= step;
            
            return value;
        }
        
        public static bool IsNullOrDestroyed(this System.Object obj) {
 
            if (object.ReferenceEquals(obj, null)) return true;
 
            if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;
 
            return false;
        }
    }
    
    public class ListQueue<T> : List<T>
    {
        new public void Add(T item) { throw new NotSupportedException(); }
        new public void AddRange(IEnumerable<T> collection) { throw new NotSupportedException(); }
        new public void Insert(int index, T item) { throw new NotSupportedException(); }
        new public void InsertRange(int index, IEnumerable<T> collection) { throw new NotSupportedException(); }
        new public void Reverse() { throw new NotSupportedException(); }
        new public void Reverse(int index, int count) { throw new NotSupportedException(); }
        new public void Sort() { throw new NotSupportedException(); }
        new public void Sort(Comparison<T> comparison) { throw new NotSupportedException(); }
        new public void Sort(IComparer<T> comparer) { throw new NotSupportedException(); }
        new public void Sort(int index, int count, IComparer<T> comparer) { throw new NotSupportedException(); }

        public void Enqueue(T item)
        {
            base.Add(item);
        }

        public T Dequeue()
        {
            var t = base[0]; 
            RemoveAt(0);
            return t;
        }

        public T Peek()
        {
            return base[0];
        }
        
        
    }

    public class UniqueList<T> : List<T>
    {
        public T PullOutRandom(System.Random rand)
        {
            var index = rand.Next(0, Count);
            var t = base[index]; 
            RemoveAt(index);
            return t;
        }

        public void RemoveElements(UniqueList<T> elements)
        {
            foreach (var element in elements)
                Remove(element);
        }
        
        public void RemoveElements(List<T> elements)
        {
            foreach (var element in elements)
                Remove(element);
        }

        public void Add(T item, Func<T, T, bool> func)
        {
            bool acted = false;
            foreach (var el in this)
            {
                if (!func(item, el))
                    acted = true;
            }
            if (!acted)
                base.Add(item);
        }
    }

    public class FloatVector
    {
        public float x;
        public float y;

        public FloatVector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        
        public double Distance(FloatVector pos)
        {
            return Math.Sqrt(Math.Pow(x - pos.x, 2) + Math.Pow(y - pos.y, 2));
        }

        public double Angle(FloatVector pos)
        {
            return Math.Atan2(y - pos.y, x - pos.x);
        }

        public IntVector ToInt()
        {
            return new IntVector((int)x, (int)y);
        }
    
        #region Operators

        public static FloatVector operator + (FloatVector a, FloatVector b)
        {
            return new FloatVector(a.x + b.x, a.y + b.y);
        }

        public static FloatVector operator - (FloatVector a, FloatVector b)
        {
            return new FloatVector(a.x - b.x, a.y - b.y);
        }

        public static FloatVector operator / (FloatVector a, float v)
        {
            return new FloatVector(a.x / v, a.y / v);
        }

        public static FloatVector operator * (FloatVector a, float v)
        {
            return new FloatVector(a.x * v, a.y * v);
        }

        #endregion
    }

    public class IntVector
    {
        public int x;
        public int y;

        public IntVector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public static class ListManager
    {
        public static UniqueList<T> Match<T, T2>(UniqueList<T> list1, UniqueList<T2> list2, Func<T, T2, bool> func)
        {
            var matches = new UniqueList<T>();
            
            foreach (var el in list1)
            {
                foreach (var el2 in list2)
                {
                    if (func(el, el2))
                        matches.Add(el);
                }
            }

            return matches;
        }
    }
}