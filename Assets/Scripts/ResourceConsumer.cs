using UnityEngine;

public enum MaterialType
{
    None,
    Sand,
    Glass,
    Color
}
public class ResourceConsumer : SnapPoint
{
    private void Start()
    {
        snapPointType = SnapPointType.ResourceConsumer;
    }
}
