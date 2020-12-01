using UnityEngine;

public class EnterExitMenu : MonoBehaviour
{
    public GameObject menuObject;
    public bool menuActivated;

    void Start()
    {
        menuActivated = false;    
    }


    void Update()
    {
        MenuActive();
        
    }
    public void OnClickEnterMenu()
    {
        
        menuActivated = true;
    }

    public void OnClickExitMenu()
    {
        menuActivated = false;
    }

    public void MenuActive()
    {
        if (menuActivated)
        {
            menuObject.SetActive(true);
            ToggleActive();
        }
        else
        {
            menuObject.SetActive(false);
        }
    }

    public void ToggleActive()
    {
        
            menuObject.SetActive(!menuObject.activeSelf);
        
    }
}
