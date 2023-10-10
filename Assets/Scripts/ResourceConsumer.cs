using UnityEngine;

public enum MaterialType
{
    Sand,
    Glass
}
public class ResourceConsumer : SnapPoint
{
    [SerializeField] private MaterialType _type;
    [SerializeField] private int _amountToCompleteLevel;

    private void OnEnable()
    {
        MaterialMovement.OnMaterialReachedEnd += ConsumeResource;
    }
    private void OnDisable()
    {
        MaterialMovement.OnMaterialReachedEnd -= ConsumeResource;
    }
    public void ConsumeResource(MaterialType type, GameObject gameObject)
    {
        if(_type == type)
        {
            _amountToCompleteLevel--;
        } 
        Destroy(gameObject);
    }
}
