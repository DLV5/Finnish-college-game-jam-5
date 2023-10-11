using System.Collections.Generic;
using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    [SerializeField] private ResourceSupplier _connectedSupplier;
    private Material _connectedSupplierMaterial;

    private List<Color> _colorsToMix = new List<Color>();

    private void Start()
    {
        _connectedSupplierMaterial = _connectedSupplier.Prefab.GetComponent<Material>();
    }
    public void  Proceed()
    { 
        if(_colorsToMix.Count > 1)
        {
            _connectedSupplierMaterial.Color = MixColors(_colorsToMix[0], _colorsToMix[1]);
            _connectedSupplierMaterial.SetColor();

            _connectedSupplier.ProduceResources = true;
        } else
        {
            _connectedSupplier.ProduceResources = false;
        }
    }

    public bool TryToAddColor(Color color1)
    {
        if(_colorsToMix.Count > 2)
        {
            _colorsToMix.RemoveAt(0);
        }

        if(_colorsToMix.Count > 1)
        {
            Proceed();
        }

        if (!_colorsToMix.Contains(color1))
        {
            _colorsToMix.Add(color1);
            return true;
        } 
        return false;
    }
    public void RemoveColor(Color color)
    {
        if (_colorsToMix.Contains(color))
        {
            _colorsToMix.Remove(color);
        }
    }
    // Function to mix two colors
    private Color MixColors(Color c1, Color c2)
    {
        Color mixedColor = new Color(
            (c1.r + c2.r) / 2f,
            (c1.g + c2.g) / 2f,
            (c1.b + c2.b) / 2f,
            (c1.a + c2.a) / 2f
        );

        return mixedColor;
    }
}