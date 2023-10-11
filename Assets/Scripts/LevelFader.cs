using UnityEngine;

public class LevelFader : MonoBehaviour
{
    public Animation levelFaderAnimation;

    public void StartFading()
    {
        // Play the fade-out animation
        levelFaderAnimation.Play("FadeOutAnimation");
    }
}