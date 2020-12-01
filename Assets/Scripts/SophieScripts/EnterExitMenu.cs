using UnityEngine;

public class EnterExitMenu : MonoBehaviour
{
    public GameObject menuObject;
    public bool menuActivated;

    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
