using UnityEngine;

public enum MaterialType
{
    None,
    Sand,
    Glass
}
public class ResourceConsumer : SnapPoint
{
    [SerializeField] private MaterialType _type;
    private void Start()
    {
        snapPointType = SnapPointType.ResourceConsumer;
    }
    public virtual void ConsumeResource(MaterialType type, GameObject gameObject)
    {     
        Destroy(gameObject);
    }
}
