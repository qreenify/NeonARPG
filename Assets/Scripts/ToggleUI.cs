using UnityEngine;

public class ToggleUI : MonoBehaviour 
{
    [Header("Inventory Toggle")]
    [SerializeField] KeyCode toggleKeyInventory = KeyCode.I;
    [SerializeField] KeyCode altToggleKeyInventory = KeyCode.B;
    [SerializeField] GameObject rootCanvas = null;

    void Awake() 
    {
        rootCanvas.SetActive(false);
    }

    void Update() 
    {
        if (Input.GetKeyDown(toggleKeyInventory) || Input.GetKeyDown(altToggleKeyInventory)) 
        {
            rootCanvas.SetActive(!rootCanvas.activeSelf);      
        }
    }
}