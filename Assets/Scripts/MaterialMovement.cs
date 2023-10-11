using UnityEngine;

public class MaterialMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private float _movementSpeed;
    private ConveyorPoint _currentPoint;
    [HideInInspector] public ConveyorPoint NextPointToMove { get; set; }

    private void Update()
    {
        if (NextPointToMove != null)
        {
            float distance = Vector3.Distance(transform.position, NextPointToMove.transform.position);

            if (distance < 0.05f)
            {
                MoveToNextPoint();
            }
            else
            {
                // Move the material towards the next point
                transform.position = Vector3.MoveTowards(transform.position, NextPointToMove.transform.position, Time.deltaTime * _movementSpeed);
            }
        } else
        {
            Destroy(gameObject);
        }
    }

    private void MoveToNextPoint()
    {
        _currentPoint = NextPointToMove;
        if (NextPointToMove.NextPoint != null)
        {
            NextPointToMove = NextPointToMove.NextPoint;
        }
        else
        {
            if(_currentPoint.Material.ConnectedConsumer != null)
            {
                _currentPoint.Material.ConnectedConsumer.ConsumeResource(_renderer.color, gameObject);
            } else
            {
                Destroy(gameObject);
            }
        }
    }
}
