using UnityEngine;

public class Finish : MonoBehaviour
{
	private Car firstFinishedCar;

	private void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player player))
		{
			if (firstFinishedCar is null)
			{
				firstFinishedCar = player.Car;
				GameManager.Instance.LevelSuccess();
			}
			else if (!firstFinishedCar.Equals(player.Car))
			{
				GameManager.Instance.LevelFail();
			}
		}
	}
}