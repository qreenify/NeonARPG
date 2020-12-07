using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SwitchScenes : MonoBehaviour
{
    //public FadeAnim fadeanim;

    public int currentScene;
    public int nextScene;

    public void SaveScene()
    {
        SaveSystem.SaveScene(this);
    }
    public void LoadScene()
    {
        SceneData data = SaveSystem.LoadScene();
        currentScene = data.scenedata;
        SceneManager.LoadScene(currentScene);

    }
    public void OnClickNewScene()
    {
        //fadeanim.StartCoroutine("Fading");
        SceneManager.LoadScene(nextScene);

    }

  
}
