using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static object _lock = new object();
	private static T instance;

	public static T Instance
	{
		get
		{
			lock (_lock)
			{
				if (instance == null) instance = (T)FindObjectOfType(typeof(T));

				return instance;
			}
		}
	}
}