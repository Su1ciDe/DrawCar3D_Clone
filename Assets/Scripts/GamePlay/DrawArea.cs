using UnityEngine;
using UnityEngine.EventSystems;

public class DrawArea : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField] private LineRenderer brush;

	[Space]
	[SerializeField] private float timeScaleWhenDrawing = .25f;

	private float distanceFromCam;
	private float previousTimeScale;

	private Car car => Player.Instance.Car;

	private RectTransform rectTransform;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	private void Start()
	{
		distanceFromCam = Vector3.Distance(GameManager.MainCamera.transform.position, transform.position) - 1;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		car.IsCarDrawn = false;
		brush.enabled = true;

		previousTimeScale = Time.timeScale;
		Time.timeScale = timeScaleWhenDrawing;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 touchPos = eventData.position;

		// Limit the drawing inside the drawing area
		if (touchPos.y > rectTransform.anchoredPosition.y)
			touchPos.y = rectTransform.anchoredPosition.y;
		else if (touchPos.y < rectTransform.anchoredPosition.y - rectTransform.rect.height)
			touchPos.y = rectTransform.anchoredPosition.y - rectTransform.rect.height;
		if (touchPos.x > rectTransform.anchoredPosition.x + rectTransform.rect.width)
			touchPos.x = rectTransform.anchoredPosition.x + rectTransform.rect.width;
		else if (touchPos.x < rectTransform.anchoredPosition.x)
			touchPos.x = rectTransform.anchoredPosition.x;

		Vector3 pos = GameManager.MainCamera.ScreenToWorldPoint((Vector3)touchPos + Vector3.forward * distanceFromCam);
		pos -= brush.transform.position;

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

	private void CreateMesh()
	{
		if (brush.positionCount < 3) return;

		Vector3[] positions = new Vector3[brush.positionCount];
		brush.GetPositions(positions);
		Vector2 offset = positions[0];
		for (int i = 0; i < positions.Length; i++)
		{
			positions[i] = Quaternion.Euler(-transform.eulerAngles) * positions[i]; // Rotate the vectors to make them 0 degree
			// To make vectors start from 0 point
			positions[i] -= (Vector3)offset;
			positions[i].z = 0;
			// Debug.Log(positions[i]);
		}

		Player.Instance.MeshGenerator.GenerateMesh(positions);

		car.SetupCar(positions[0], positions[positions.Length - 1]);
	}
}