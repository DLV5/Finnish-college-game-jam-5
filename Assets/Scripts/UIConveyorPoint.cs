using UnityEngine;

[RequireComponent (typeof(ConveyorPoint))]
public class UIConveyorPoint : MonoBehaviour
{
    private ConveyorPoint _ConveyorPoint;

    [SerializeField] private GameObject _uiContainer;
    // Start is called before the first frame update
    void Start()
    {
        _ConveyorPoint = GetComponent<ConveyorPoint>();
    }

    public void ToggleUIContainer()
    {
        _uiContainer.SetActive (!_uiContainer.activeSelf);
    }
}
