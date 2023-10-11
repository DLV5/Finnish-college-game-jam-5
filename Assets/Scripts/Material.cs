using UnityEngine;

public class Material : MonoBehaviour
{
    [HideInInspector] public Color Color;
    [SerializeField] private SpriteRenderer _renderer;
    public void SetColor()
    {
        _renderer.color = Color;
    }
}
