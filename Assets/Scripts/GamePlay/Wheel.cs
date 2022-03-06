using UnityEngine;

public class Wheel : MonoBehaviour
{
	public WheelCollider WheelCollider;
	public Transform WheelObject;

	private void Start()
	{
		WheelCollider.steerAngle = 90;
	}

	private void FixedUpdate()
	{
		if (!gameObject.activeSelf) return;

		WheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
		WheelObject.position = pos;
		WheelObject.rotation = rot;
	}
}