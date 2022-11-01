using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;
	public static T instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<T>();
				if (_instance == null)
				{
					GameObject obj = new GameObject(nameof(T));
					_instance = obj.AddComponent<T>();
				}
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(gameObject);
			return;
		}

		_instance = GetComponent<T>();
		DontDestroyOnLoad(gameObject);
		OnAwake();
	}
	
	protected virtual void OnAwake(){}
}
