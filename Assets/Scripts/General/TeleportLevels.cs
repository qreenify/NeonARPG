using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportLevels : MonoBehaviour
{
    public void onClickEnterScene(int Scene)
    {
        SceneManager.LoadScene(Scene);
    } 

}
