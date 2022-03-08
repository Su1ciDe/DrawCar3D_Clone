using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSuccessScreen : MonoBehaviour
{
	public void NextLevel()
	{
		// Reload same scene for testing purposes
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		GameManager.Instance.StartLevel();
		gameObject.SetActive(false);
	}
}