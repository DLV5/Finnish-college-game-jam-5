using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject _conveyorPointPrefab;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float _overlapCircleRadius;
    private void OnMouseDown()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        position.z = 0;
        if (!CheckAround(position))
        {
            Instantiate( _conveyorPointPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
        }
    }

    private bool CheckAround(Vector3 position)
    {
        return Physics2D.OverlapCircle(position, _overlapCircleRadius, layerMask) != null;
    }
}
