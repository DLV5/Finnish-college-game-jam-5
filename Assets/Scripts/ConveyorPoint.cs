using UnityEngine;

public class ConveyorPoint : MonoBehaviour
{
    public bool IsFirstPointOfALine { get; set; } = true;
    public ConveyorPoint NextPoint { get; private set; }
    public ConveyorPointMaterial Material;
    public bool FollowMouse { get; set; } = false;
    private UIConveyorPoint _uiConveyorPoint;
    private LineRenderer _lineRenderer;

    private Vector3 initialMousePosition;
    private bool _isDragging = false;
    private bool _isLastPointOfALine = true;

    private SnapPoint _connectedSnapPoint;

    [SerializeField] private float _snapRadius;


    private void Start()
    {
        _lineRenderer ??= GetComponent<LineRenderer>();
        _uiConveyorPoint = GetComponent<UIConveyorPoint>();
        Material = GetComponent<ConveyorPointMaterial>();
    }

    private void Update()
    {
        if(_lineRenderer != null)
        {
            UpdateLineRendererPositions();
        }

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
            if(_connectedSnapPoint != null)
            {
                Material.ConnectedConsumer = null;
                _connectedSnapPoint.ConnectedConveyorPoint = null;
                _connectedSnapPoint = null;
            }
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
        // Find all snap points in the scene of the specified type
        SnapPoint[] snapPoints = FindObjectsOfType<SnapPoint>();

        if (snapPoints.Length == 0)
        {
            return; // No snap points found, exit the function
        }

        Vector3 currentPosition = transform.position;
        SnapPoint nearestSnapPoint = null;
        float closestDistance = _snapRadius; // Set an initial distance greater than the snapping radius

        foreach (SnapPoint snapPoint in snapPoints)
        {
            // Check if the snap point matches the desired type
            if (snapPoint.snapPointType == snapPointType)
            {
                float distance = Vector3.Distance(currentPosition, snapPoint.transform.position);

                // If this snap point is within the snapping radius and closer than the previous one
                if (distance <= _snapRadius && distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestSnapPoint = snapPoint;
                }
            }
        }

        // Snap to the nearest snap point if one was found
        if (nearestSnapPoint != null && nearestSnapPoint.ConnectedConveyorPoint == null)
        {
            transform.position = nearestSnapPoint.transform.position;
            _connectedSnapPoint = nearestSnapPoint.GetComponent<SnapPoint>();
            _connectedSnapPoint.ConnectedConveyorPoint = this;
            if (snapPointType == SnapPointType.ResourceConsumer)
            {
                Material.ConnectedConsumer = nearestSnapPoint.GetComponent<ColorConsumer>();
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
