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
}