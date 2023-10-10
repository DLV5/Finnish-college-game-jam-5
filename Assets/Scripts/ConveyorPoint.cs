using UnityEngine;
[RequireComponent (typeof(UIConveyorPoint))]
public class ConveyorPoint : MonoBehaviour
{
    private UIConveyorPoint _uiConveyorPoint;

    private ConveyorPoint _nextPoint;

    private LineRenderer _lineRenderer;


    private Vector3 initialMousePosition;
    private bool _isDragging = false;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _uiConveyorPoint = GetComponent<UIConveyorPoint>();
    }

        private void Update()
    {
        UpdateLineRendererPositions();
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");

        initialMousePosition = GetMouseWorldPosition();
        _isDragging = false;
    }

    private void OnMouseDrag()
    {
        float distance = Vector3.Distance(initialMousePosition, GetMouseWorldPosition());

        // If the distance exceeds a threshold, consider it a drag
        if (distance > 0.1f)
        {
            _isDragging = true;
        }

        // If it's a drag, move the point
        if (_isDragging)
        {
            transform.position = GetMouseWorldPosition();
            Debug.Log("OnMouseDrag");

        }
    }

    private void OnMouseUp()
    {
        if (!_isDragging)
        {
            _uiConveyorPoint.ToggleUIContainer();
        }

        _isDragging = false;
    }

    private void CreateNextPoint()
    {
        _nextPoint = Instantiate(gameObject, GetMouseWorldPosition(), Quaternion.identity).GetComponent<ConveyorPoint>();
        _nextPoint.gameObject.transform.position = GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        position.z = 0;

        return position;
    }


    private void UpdateLineRendererPositions()
    {
        if (_nextPoint != null)
        {
            // Set the positions of the LineRenderer to match the positions of pointA and pointB
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _nextPoint.transform.position);
        } else
        {
            _lineRenderer.enabled = false;
        }
    }
}
