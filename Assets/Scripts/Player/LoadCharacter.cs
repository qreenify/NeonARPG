using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public GameObject [] playerPrefabs;

    private void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        GameObject prefab = playerPrefabs[selectedCharacter];
        GameObject cloneOfPlayer = Instantiate(prefab, transform.position, transform.rotation);
        cloneOfPlayer.transform.parent = gameObject.transform;
    }

    /*Source:
     * Unity Tutorial - Simple Character Selection System
     * https://www.youtube.com/watch?v=3qlRgICRoeA&ab_channel=RumpledCode 
     */
}
