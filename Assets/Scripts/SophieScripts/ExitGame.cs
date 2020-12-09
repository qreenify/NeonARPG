using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public float exitTime = 1;
    public float exitStartTime;
    public void Awake()
    {
        enabled = false;
    }
    public void Update()
    {
        if(Time.time - exitStartTime > exitTime)
        {
            Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }
    }
    public void Quit()
    {
        exitStartTime = Time.time;
        enabled = true;
    }
}
