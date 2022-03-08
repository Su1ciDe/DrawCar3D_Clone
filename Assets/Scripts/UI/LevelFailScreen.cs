using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailScreen : MonoBehaviour
{
	public void RetryLevel()
	{
		// Reload same scene for testing purposes
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		GameManager.Instance.StartLevel();
		gameObject.SetActive(false);
	}
}