using System;
using UnityEngine;

public enum SnapPointType
{
    None,           // Default
    ResourceSupplier,
    ResourceConsumer
}

public class SnapPoint : MonoBehaviour
{
    protected event Action OnConveyorPointDisconnected;

    private ConveyorPoint _connectedConveyorPoint;
    public ConveyorPoint ConnectedConveyorPoint 
    {
        get => _connectedConveyorPoint; 
        set
        {
            _connectedConveyorPoint = value;
            if(_connectedConveyorPoint == null)
            {
                OnConveyorPointDisconnected?.Invoke();
            }
        }
    } // Reference to the connected ConveyorPoint
    [HideInInspector] public SnapPointType snapPointType = SnapPointType.None; // Type of snap point
}