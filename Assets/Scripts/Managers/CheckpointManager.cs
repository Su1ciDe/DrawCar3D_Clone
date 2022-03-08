using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
	public Checkpoint[] Checkpoints { get; private set; }
	public Checkpoint CurrentCheckpoint { get; set; }

	public Material ActivatedMat;

	private void Awake()
	{
		InitCheckpoints();
	}

	private void InitCheckpoints()
	{
		Checkpoints = new Checkpoint[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			var checkpoint = transform.GetChild(i).GetComponent<Checkpoint>();
			checkpoint.Index = (uint)i;
			Checkpoints[i] = checkpoint;
		}
	}

	public void StartFromCheckpoint()
	{
		Player.Instance.transform.position = CurrentCheckpoint ? CurrentCheckpoint.transform.position : Vector3.zero;
		Player.Instance.transform.eulerAngles = Vector3.zero;
		Player.Instance.Rb.velocity = Vector3.zero;
	}
}