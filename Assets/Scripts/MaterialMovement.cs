using System;
using UnityEngine;

public class MaterialMovement : MonoBehaviour
{
    public static event Action<MaterialType, GameObject> OnMaterialReachedEnd;

    [SerializeField] private MaterialType _materialType;

    [SerializeField] private float _movementSpeed;
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
        }
    }

    private void MoveToNextPoint()
    {
        if (NextPointToMove.NextPoint != null)
        {
            NextPointToMove = NextPointToMove.NextPoint;
        }
        else
        {
            OnMaterialReachedEnd?.Invoke(_materialType, gameObject);
        }
    }
}
