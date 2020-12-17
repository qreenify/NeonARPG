using UnityEngine;

public class PortalState : MonoBehaviour
{
    public bool defaultState;
    public string portalName;
    private GameObject _portal;

    private bool IsOpen
    {
        get
        {
            var defaultState = 0;
            if (this.defaultState)
            {
                defaultState = 1;
            }
            var state = PlayerPrefs.GetInt(gameObject.name + "_state", defaultState);
            return state != 0;
        }
        set
        {
            var state = 0;
            if (value)
            {
                state = 1;
            }
            PlayerPrefs.SetInt(gameObject.name + "_state", state);
        }
    }

    private void Start()
    {
        _portal = GetComponentInChildren<Portal>(true).gameObject;
        _portal.SetActive(IsOpen);
    }
}
