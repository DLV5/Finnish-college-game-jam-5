using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour
{
    public Animator levelFaderAnimation;

    public void StartFading()
    {
        // Play the fade-out animation
        levelFaderAnimation.SetTrigger("FadeIn");
    }

    public void FadeToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}