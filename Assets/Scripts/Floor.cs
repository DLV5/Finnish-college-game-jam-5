using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject _conveyorPointPrefab;
    private void OnMouseDown()
    {
        Instantiate( _conveyorPointPrefab, Input.mousePosition, Quaternion.identity);
    }
}
