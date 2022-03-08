using UnityEngine;

public class Booster : MonoBehaviour
{
	[SerializeField] private float boostForce = 20000;

	private void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player player))
		{
			player.Rb.AddForce(Vector3.right * boostForce);
		}
	}
}