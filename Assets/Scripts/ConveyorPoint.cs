using UnityEngine;

public class ConveyorPoint : MonoBehaviour
{
    private UIConveyorPoint _uiConveyorPoint;
    private LineRenderer _lineRenderer;

    private Vector3 initialMousePosition;
    private bool _isDragging = false;
    private bool _isLastPointOfALine = false;

    [SerializeField] private float _snapRadius;

    public bool IsFirstPointOfALine { get; set; } = true;
    public ConveyorPoint NextPoint { get; private set; }
    public bool FollowMouse { get; set; } = false;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _uiConveyorPoint = GetComponent<UIConveyorPoint>();
    }

    private void Update()
    {
        UpdateLineRendererPositions();

        if (FollowMouse)
        {
            MoveToMouse();
        }
    }

    private void OnMouseDown()
    {
        initialMousePosition = GetMouseWorldPosition();
        _isDragging = false;

        if (FollowMouse)
        {
            FollowMouse = false;
        }
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
            MoveToMouse();
        }
    }

    private void OnMouseUp()
    {
        // Check if it's the last point of a line

        // Handle UI toggle
        if (!_isDragging)
        {
            _uiConveyorPoint.ToggleUIContainer();
        }

        if (IsFirstPointOfALine)
        {
            SnapToNearestSnapPoint(SnapPointType.ResourceSupplier);
        }
        else if (_isLastPointOfALine)
        {
            SnapToNearestSnapPoint(SnapPointType.ResourceConsumer);
        }

        _isDragging = false;
    }

    private void SnapToNearestSnapPoint(SnapPointType snapPointType)
    {
        Vector2 currentPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(currentPosition, _snapRadius);

        foreach (Collider2D collider in colliders)
        {
            SnapPoint snapPoint = collider.GetComponent<SnapPoint>();

            if (snapPoint != null && snapPoint.snapPointType == snapPointType)
            {
                transform.position = snapPoint.transform.position;
                break; // Stop after snapping to the nearest snap point
            }
        }
    }

    public void CreateNextPoint()
    {
        if (NextPoint != null)
        {
            NextPoint.DeleteConveyorPoint();
        }
        _isLastPointOfALine = false;

        NextPoint = Instantiate(gameObject, GetMouseWorldPosition(), Quaternion.identity).GetComponent<ConveyorPoint>();
        NextPoint.FollowMouse = true;
        NextPoint.IsFirstPointOfALine = false;
    }

    public void DeleteConveyorPoint()
    {
        if (NextPoint != null)
        {
            NextPoint.DeleteConveyorPoint();
        }

        Destroy(gameObject);
    }

    private void MoveToMouse()
    {
        transform.position = GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        return position;
    }

    private void UpdateLineRendererPositions()
    {
        if (NextPoint != null)
        {
            // Set the positions of the LineRenderer to match the positions of pointA and pointB
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, NextPoint.transform.position);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
