using System.Text.Json;
using System.Text.Json.Nodes;

namespace PipeCommon.Extensions;

public static class DictionaryExtensions
{
    public static void AddRange<T, T1>(this IDictionary<T, T1> target, IDictionary<T, T1> source)
    {
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        foreach (var element in source)
        {
            target.Add(element);
        }
    }

    /// <summary>
    /// Получение значения из словаря в правильном типе
    /// </summary>    
    public static bool TryGetValue<T, TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out T value)
    {
        if (dictionary.TryGetValue(key, out var _val))
        {
            if (_val is T)
            {
                value = (T)Convert.ChangeType(_val, typeof(T));
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }
        value = default;
        return false;
    }

    /// <summary>
    /// Получение коллекций из словаря по ключу
    /// </summary>
    public static bool TryGetValues<T, TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out IEnumerable<T> values)
    {
        if (dictionary.TryGetValue(key, out var _val))
        {
            switch (_val)
            {
                case JsonArray jValues:
                    values = JsonSerializer.Deserialize<IEnumerable<T>>(jValues.ToJsonString());
                    return true;

                case T[] arrValues:
                    values = arrValues;
                    return true;

                case IEnumerable<T> enumerableValues:
                    values = enumerableValues;
                    return true;

                default:
                    values = default;
                    return false;
            }
        }

        values = default;
        return false;
    }

    /// <summary>
    /// Метод получения из словаря значения по ключу, если объект не null
    /// </summary>
    public static T TryGetValue<T>(this T t, IDictionary<Guid, T> dictionary, Guid key) where T : class
    {
        if (t == null)
        {
            dictionary.TryGetValue(key, out t);
        }
        return t;
    }

    /// <summary>
    /// Получение значения из словаря по ключу
    /// </summary>
    public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : class
    {
        dictionary.TryGetValue(key, out var value);
        return value;
    }

    /// <summary>
    /// Получение массива значений из словаря по ключу
    /// </summary>
    public static bool GetArray<TValue>(this IDictionary<string, object> dict, string key, out TValue[] value)
    {
        if (dict.TryGetValues(key, out IEnumerable<TValue> tIds) && tIds.Any())
        {
            value = tIds.ToArray();
            return true;
        }
        if (dict != null && dict.TryGetValue(key, out object ids) && !string.IsNullOrEmpty(ids.ToString()))
        {
            value = JsonSerializer.Deserialize<TValue[]>(ids.ToString());
            if (value?.Length > 0)
            {
                return true;
            }
        }
        value = null;
        return false;
    }
}