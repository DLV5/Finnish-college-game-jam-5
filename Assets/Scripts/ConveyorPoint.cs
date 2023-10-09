using UnityEngine;

public class ConveyorPoint : MonoBehaviour
{
    private ConveyorPoint _nextPoint;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

        private void Update()
    {
        UpdateLineRendererPositions();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        //CreateNextPoint();
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
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _nextPoint.transform.position);
        } else
        {
            lineRenderer.enabled = false;
        }
    }
}
