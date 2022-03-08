using DG.Tweening;
using UnityEngine;

public class MovingObstacle : Obstacle
{
	[SerializeField] private Vector3 direction = Vector3.up;
	[SerializeField] private float distance = 1;
	[SerializeField] private float time = 1;

	private void Start()
	{
		transform.DOMove(direction * distance, time).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetRelative();
	}
}