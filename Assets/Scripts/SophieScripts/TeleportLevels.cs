using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportLevels : MonoBehaviour
{
    public int Scenes = 1;

    public void onClickEnterScene()
    {
        SceneManager.LoadScene(Scenes);
    } 

}
