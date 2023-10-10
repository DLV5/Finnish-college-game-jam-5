using UnityEngine;
[RequireComponent (typeof(UIConveyorPoint))]
public class ConveyorPoint : MonoBehaviour
{
    private UIConveyorPoint _uiConveyorPoint;


    private LineRenderer _lineRenderer;


    private Vector3 initialMousePosition;
    private bool _isDragging = false;

    private bool _isLastPointOfALine = false;

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

        if(FollowMouse)
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
        if(NextPoint != null)
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
        } else
        {
            _lineRenderer.enabled = false;
        }
    }
}
