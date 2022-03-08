using UnityEngine;

public class Player : Singleton<Player>
{
	public Rigidbody Rb { get; private set; }

	public Car Car { get; private set; }
	public MeshGenerator MeshGenerator { get; private set; }

	private DrawArea drawArea => DrawArea.Instance;

	private void Awake()
	{
		Rb = GetComponent<Rigidbody>();
		Car = GetComponentInChildren<Car>();
		MeshGenerator = GetComponentInChildren<MeshGenerator>();
	}

	private void OnEnable()
	{
		GameManager.OnLevelStart += OnLevelStarted;
		GameManager.OnLevelSuccess += OnLevelSuccess;
		GameManager.OnLevelFail += OnLevelFailed;
	}

	private void OnDisable()
	{
		GameManager.OnLevelStart -= OnLevelStarted;
		GameManager.OnLevelSuccess -= OnLevelSuccess;
		GameManager.OnLevelFail -= OnLevelFailed;
	}

	private void OnLevelStarted()
	{
		drawArea.gameObject.SetActive(true);
		drawArea.CanDraw = true;
	}

	private void OnLevelSuccess()
	{
		drawArea.CanDraw = false;
		drawArea.gameObject.SetActive(false);

		Car.Brake();
	}

	private void OnLevelFailed()
	{
		drawArea.CanDraw = false;
		drawArea.gameObject.SetActive(false);
		
		Car.Brake();
	}
}