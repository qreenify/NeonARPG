using UnityEngine;
using UnityEngine.SceneManagement;


public class SwitchScenes : MonoBehaviour
{     
    public int index = 1;
    //public FadeAnim fadeanim;
   public void OnClickNewScene()
    {
        //fadeanim.StartCoroutine("Fading");
        SceneManager.LoadScene(index);

    }


}
