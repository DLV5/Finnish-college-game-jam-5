using UnityEngine;

[RequireComponent (typeof(ConveyorPoint))]
public class UIConveyorPoint : MonoBehaviour
{
    [SerializeField] private GameObject _uiContainer;

    public void ToggleUIContainer()
    {
        _uiContainer.SetActive (!_uiContainer.activeSelf);
    }
}
