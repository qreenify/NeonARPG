using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelection : MonoBehaviour
{

    public GameObject[] characters;
    public int selectedCharacter = 0;
    public int index;
    public void OnClickNextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if(selectedCharacter > characters.Length -1)
        {
            selectedCharacter = 0;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void OnClickPreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }

        characters[selectedCharacter].SetActive(true);

    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        SceneManager.LoadScene(index);
    }

    /*Source:
     * Unity Tutorial - Simple Character Selection System
     * https://www.youtube.com/watch?v=3qlRgICRoeA&ab_channel=RumpledCode
     
     */
}
