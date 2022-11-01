using UnityEngine;

public class Config<T> : ScriptableObject where T : Config<T>
{
    private static string _nameOfType;

    static string nameOfType
    {
        get
        {
            if (_nameOfType == null)
            {
                _nameOfType = typeof(T).Name;
            }

            return _nameOfType;
        }
    }
    public static T instance =>
        _loaded ? _loaded : _loaded = Resources.Load<T>("Config/" + nameOfType);
    private static T _loaded;
}
