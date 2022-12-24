namespace Base.Extensions;

public static class DictionaryExtension
{
    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> sourceDict,
        Dictionary<TKey, TValue> otherDictionary, Func<IEnumerable<TValue>, TValue> mergeFunc)
        where TKey : notnull
    {
        return sourceDict.Merge(new[] { otherDictionary }, mergeFunc);
    }

    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> sourceDict,
        IEnumerable<Dictionary<TKey, TValue>> otherDictionaries, Func<IEnumerable<TValue>, TValue> mergeFunc)
        where TKey : notnull
    {
        var dictionaries = new List<Dictionary<TKey, TValue>> { sourceDict };
        dictionaries.AddRange(otherDictionaries);
        return dictionaries
            .SelectMany(dict => dict)
            .GroupBy(pair => pair.Key)
            .ToDictionary(
                group => group.Key,
                dict => mergeFunc(dict.Select(pair => pair.Value))
            );
    }

    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(IEnumerable<Dictionary<TKey, TValue>> otherDictionaries,
        Func<IEnumerable<TValue>, TValue> mergeFunc)
        where TKey : notnull
    {
        return new Dictionary<TKey, TValue>().Merge(otherDictionaries, mergeFunc);
    }

    public static void AddMerge<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value,
        Func<TValue, TValue, TValue> mergeFunc)
        where TKey : notnull
    {
        if (dict.ContainsKey(key))
        {
            dict[key] = mergeFunc(dict[key], value);
        }
        else
        {
            dict.Add(key, value);
        }
    }
}