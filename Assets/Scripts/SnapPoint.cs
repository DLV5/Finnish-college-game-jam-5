using UnityEngine;

public enum SnapPointType
{
    None,           // Default
    ResourceSupplier,
    ResourceConsumer
}

public class SnapPoint : MonoBehaviour
{
    public ConveyorPoint ConnectedConveyorPoint { get; set; } // Reference to the connected ConveyorPoint
    [HideInInspector] public SnapPointType snapPointType = SnapPointType.None; // Type of snap point
}