using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public static Camera MainCamera;

	private void Awake()
	{
		MainCamera = Camera.main;
	}
}
