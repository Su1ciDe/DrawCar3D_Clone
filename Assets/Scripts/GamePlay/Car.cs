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

	private void Update()
	{
		Player.Instance.transform.eulerAngles = Vector3.forward * Player.Instance.transform.eulerAngles.z;
	}

	private void FixedUpdate()
	{
		Drive();
	}

	public void SetupCar(Vector3 firstWheelPos, Vector3 lastWheelPos)
	{
		Player.Instance.transform.eulerAngles = Vector3.zero;
		transform.eulerAngles = Vector3.zero;
		
		wheels[0].transform.localPosition = firstWheelPos;
		wheels[1].transform.localPosition = lastWheelPos;
		wheels[0].gameObject.SetActive(true);
		wheels[1].gameObject.SetActive(true);
		wheels[0].ResetSprungMasses();
		wheels[1].ResetSprungMasses();

		// if wheel position is under the road, set the player position above it
		float y = lastWheelPos.y < 0 ? Mathf.Abs(lastWheelPos.y) : 0;

		Player.Instance.transform.position = new Vector3(Player.Instance.transform.position.x, y, Player.Instance.transform.position.z);
		
		Player.Instance.Rb.isKinematic = false;
		IsCarDrawn = true;
	}

	private void InitWheels()
	{
		for (int i = 0; i < wheels.Length; i++)
		{
			var wheel = Instantiate(wheelPrefab, transform);
			wheel.gameObject.SetActive(false);
			wheels[i] = wheel;
		}
	}

	private void Drive()
	{
		if (IsCarDrawn)
			WheelTorque(speed);
		else
			WheelTorque(0);
	}

	private void WheelTorque(float _speed)
	{
		for (int i = 0; i < wheels.Length; i++)
			wheels[i].WheelCollider.motorTorque = _speed;
	}
}