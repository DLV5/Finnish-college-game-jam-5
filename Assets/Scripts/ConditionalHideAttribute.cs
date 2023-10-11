using UnityEngine;

public class ConditionalHideAttribute : PropertyAttribute
{
    public string ConditionalSourceField;
    public bool Inverse = false;

    public ConditionalHideAttribute(string conditionalSourceField, bool inverse = false)
    {
        ConditionalSourceField = conditionalSourceField;
        Inverse = inverse;
    }
}
