using UnityEngine;

public class EnterExitMenu : MonoBehaviour
{
    PauseGame pauseGame;

    void Awake()
    {
        pauseGame = GetComponent<PauseGame>();
    }
    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        pauseGame.PauseObjectActivated();
    }
}
