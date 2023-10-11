using System;
using UnityEngine;

public class ColorConsumer : ResourceConsumer
{
    [SerializeField] private ColorMixer _connectedMixer;
    private Color _lastColor;

    private void OnEnable()
    {
        OnConveyorPointDisconnected += RemoveLastColor;
    }

    private void RemoveLastColor()
    {
        _connectedMixer.RemoveColor(_lastColor);
    }

    public void ConsumeResource(Color color, GameObject gameObject)
    {
        _lastColor = color;
        _connectedMixer.TryToAddColor(color);
        Destroy(gameObject);
    }
}
