using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public GameObject damagePopup;

    private void Awake()
    {
        if (_i == null)
        {
            _i = this;
        }
        else
            Destroy(gameObject);
    }

    public static GameAssets Instance => _i;
}


