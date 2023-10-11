using UnityEngine;

public class ConveyorPointMaterial : MonoBehaviour
{
    private ConveyorPoint _conveyorPoint;

    public MaterialType resource; // Reference to the resource carried by the conveyor point
    public ResourceConsumer ConnectedConsumer { get; set; }
    void Start()
    {
        _conveyorPoint = GetComponent<ConveyorPoint>();
    }

}
