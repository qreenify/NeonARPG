using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public AudioSource endGameSound;
    public void Quit()
    {
        if(endGameSound != null)
            endGameSound.Play();
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
