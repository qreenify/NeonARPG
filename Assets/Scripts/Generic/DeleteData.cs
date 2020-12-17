using UnityEngine;

public class DeleteData : MonoBehaviour
{
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
