using System;
using System.Collections.Generic;

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
