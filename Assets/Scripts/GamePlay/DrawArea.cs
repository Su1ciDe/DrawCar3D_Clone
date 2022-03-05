using UnityEngine;
using UnityEngine.EventSystems;

public class DrawArea : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField] private LineRenderer brush;

	private float distanceFromCam;
	private float previousTimeScale;

	[Header("Limits")]
	[SerializeField] private RectTransform top;
	[SerializeField] private RectTransform bottom;
	[SerializeField] private RectTransform left;
	[SerializeField] private RectTransform right;

	private Car car => Player.Instance.Car;

	private void Start()
	{
		distanceFromCam = Vector3.Distance(GameManager.MainCamera.transform.position, transform.position) - 1.5f;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		brush.enabled = true;

		previousTimeScale = Time.timeScale;
		Time.timeScale *= .5f;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 touchPos = eventData.position;

		Vector3 pos = GameManager.MainCamera.ScreenToWorldPoint((Vector3)touchPos + Vector3.forward * distanceFromCam);

		Draw(pos);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		CreateMesh();

		brush.positionCount = 0;
		brush.enabled = false;

		Time.timeScale = previousTimeScale;
	}

	private void Draw(Vector3 pos)
	{
		brush.positionCount++;
		brush.SetPosition(brush.positionCount - 1, pos);
	}

	public void CreateMesh()
	{
		Vector3[] positions = new Vector3[brush.positionCount];
		brush.GetPositions(positions);
		Player.Instance.MeshGenerator.GenerateMesh(positions);

		car.transform.eulerAngles = -transform.eulerAngles;

		Player.Instance.Rb.isKinematic = false;
	}
}