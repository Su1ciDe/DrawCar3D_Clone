using UnityEngine;

public class FallTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player _))
		{
			CheckpointManager.Instance.StartFromCheckpoint();
		}
	}
}