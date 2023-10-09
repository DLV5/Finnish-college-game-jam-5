using Unity.VisualScripting;
using UnityEngine;

public class ConveyorPoint : MonoBehaviour
{
    private ConveyorPoint _nextPoint;
    private void OnMouseDrag()
    {
        //transform.position = GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        CreateNextPoint();
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
}
