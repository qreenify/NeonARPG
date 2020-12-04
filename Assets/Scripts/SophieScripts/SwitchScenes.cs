using UnityEngine;
using UnityEngine.SceneManagement;


public class SwitchScenes : MonoBehaviour
{     
    //public FadeAnim fadeanim;
   public void OnClickNewScene(int index)
    {
        //fadeanim.StartCoroutine("Fading");
        SceneManager.LoadScene(index);

    }


}
