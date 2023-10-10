using UnityEngine;

public class MaterialMovement : MonoBehaviour
{
    [SerializeField] private ConveyorPoint _nextPointToMove;
    [SerializeField] private float _movementSpeed;

    private void Update()
    {
        if (_nextPointToMove != null)
        {
            float distance = Vector3.Distance(transform.position, _nextPointToMove.transform.position);

            if (distance < 0.05f)
            {
                MoveToNextPoint();
            }
            else
            {
                // Move the material towards the next point
                transform.position = Vector3.MoveTowards(transform.position, _nextPointToMove.transform.position, Time.deltaTime * _movementSpeed);
            }
        }
    }

    private void MoveToNextPoint()
    {
        if (_nextPointToMove.NextPoint != null)
        {
            _nextPointToMove = _nextPointToMove.NextPoint;
        }
        else
        {
            HandleEndOfConveyor();
        }
    }

    private void HandleEndOfConveyor()
    {
        Debug.Log("Reached end");
        Destroy(gameObject);
    }
}
