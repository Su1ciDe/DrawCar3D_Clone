using UnityEngine;

public class Player : Singleton<Player>
{
	public Rigidbody Rb { get; private set; }
	
	public Car Car { get; private set; }
	public MeshGenerator MeshGenerator { get; private set; }
	

	private void Awake()
	{
		Rb = GetComponent<Rigidbody>();
		Car = GetComponentInChildren<Car>();
		MeshGenerator = GetComponentInChildren<MeshGenerator>();
	}
}