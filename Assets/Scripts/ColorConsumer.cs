using System;
using UnityEngine;

public class ColorConsumer : ResourceConsumer
{
    [ConditionalHide("_isFinal", true), SerializeField] private ColorMixer _connectedMixer;
    private Color _lastColor;

    [SerializeField] private bool _isFinal = false;
    [ConditionalHide("_isFinal"), SerializeField] private Color _requiredColor;
    [ConditionalHide("_isFinal"), SerializeField] private GameObject _levelFinishedUI;
    [ConditionalHide("_isFinal"), SerializeField] private float _threshold = 0.1f;


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
        if(!_isFinal) { 
            _lastColor = color;
            _connectedMixer.TryToAddColor(color);
            Destroy(gameObject);
        }
        else
        {
            CheckColor(color);
        }
    }

    private void CheckColor(Color color)
    {
        if(CompareColors(_requiredColor, color, _threshold))
        {
            _levelFinishedUI.SetActive(true);
        }
    }

    private bool CompareColors(Color c1, Color c2, float threshold)
    {
        float rDiff = c1.r - c2.r;
        float gDiff = c1.g - c2.g;
        float bDiff = c1.b - c2.b;

        // Calculate the Euclidean distance between the two colors
        float distance = Mathf.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);

        // Check if the distance is below the threshold
        return distance <= threshold;
    }
}
