using UnityEngine;

public enum SnapPointType
{
    None,           // Default
    ResourceSupplier,
    ResourceConsumer
}

public class SnapPoint : MonoBehaviour
{
    public ConveyorPoint connectedConveyorPoint; // Reference to the connected ConveyorPoint
    public SnapPointType snapPointType = SnapPointType.None; // Type of snap point

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if an object (e.g., conveyor point) has entered the snap point
        ConveyorPoint conveyorPoint = other.GetComponent<ConveyorPoint>();
        if (conveyorPoint != null)
        {
            // Connect the conveyor point to this snap point
            connectedConveyorPoint = conveyorPoint;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if an object (e.g., conveyor point) has exited the snap point
        ConveyorPoint conveyorPoint = other.GetComponent<ConveyorPoint>();
        if (conveyorPoint != null && conveyorPoint == connectedConveyorPoint)
        {
            // Disconnect the conveyor point from this snap point
            connectedConveyorPoint = null;
        }
    }
}