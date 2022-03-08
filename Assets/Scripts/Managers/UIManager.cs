using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	public LevelSuccessScreen LevelSuccessScreen => levelSuccessScreen ? levelSuccessScreen : levelSuccessScreen = GetComponentInChildren<LevelSuccessScreen>(true);
	private LevelSuccessScreen levelSuccessScreen;

	public LevelFailScreen LevelFailScreen => levelFailScreen ? levelFailScreen : levelFailScreen = GetComponentInChildren<LevelFailScreen>(true);
	private LevelFailScreen levelFailScreen;
	
}