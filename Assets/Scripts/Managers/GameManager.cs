using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
	public static event UnityAction OnLevelStart, OnLevelSuccess, OnLevelFail;

	public static Camera MainCamera;

	private void Awake()
	{
		MainCamera = Camera.main;
	}

	private void Start()
	{
		StartLevel();
	}

	public void StartLevel()
	{
		OnLevelStart?.Invoke();
	}

	public void LevelSuccess()
	{
		OnLevelSuccess?.Invoke();
	}

	public void LevelFail()
	{
		OnLevelFail?.Invoke();
	}
}