using System;

public static class ListUtils
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
