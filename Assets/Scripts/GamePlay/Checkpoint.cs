using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public uint Index { get; set; }

	[SerializeField] private Transform checkpointFlag;

	private MeshRenderer meshRenderer;

	private void Awake()
	{
		meshRenderer = checkpointFlag.GetComponent<MeshRenderer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player _))
		{
			if (!CheckpointManager.Instance.CurrentCheckpoint)
				Activate();
			else if (CheckpointManager.Instance.CurrentCheckpoint.Index < Index)
				Activate();
		}
	}

	private void Activate()
	{
		CheckpointManager.Instance.CurrentCheckpoint = this;

		meshRenderer.material = CheckpointManager.Instance.ActivatedMat;
	}
}