using UnityEngine;

public class Car : MonoBehaviour
{
	public bool IsCarDrawn { get; set; }

	[SerializeField] private Wheel wheelPrefab;
	private Wheel[] wheels = new Wheel[2];

	[SerializeField] private float speed = 10;

	private void Awake()
	{
		InitWheels();
	}

	private void FixedUpdate()
	{
		Drive();
	}

	public void SetupCar(Vector3 firstWheelPos, Vector3 lastWheelPos)
	{
		wheels[0].transform.localPosition = firstWheelPos;
		wheels[1].transform.localPosition = lastWheelPos;
		wheels[0].gameObject.SetActive(true);
		wheels[1].gameObject.SetActive(true);

		transform.eulerAngles = Vector3.zero;
		
		Player.Instance.Rb.isKinematic = false;

		IsCarDrawn = true;
	}

	private void InitWheels()
	{
		for (int i = 0; i < 2; i++)
		{
			var wheel = Instantiate(wheelPrefab, transform);
			wheel.gameObject.SetActive(false);
			wheels[i] = wheel;
		}
	}

	private void Drive()
	{
		if (!IsCarDrawn) return;

		for (int i = 0; i < wheels.Length; i++)
			wheels[i].WheelCollider.motorTorque = speed;
	}
}