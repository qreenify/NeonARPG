using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour
{
    public static LevelFader levelFader;
    CanvasGroup canvasGroup;
    [Range(0.1f, 5.0f)]
    [SerializeField] float fadeOutTime = 0.5f;
    [Range(0.1f, 5.0f)]
    [SerializeField] float darkTime = 1f;
    [Range(0.1f, 5.0f)]
    [SerializeField] float fadeInTime = 1f;
    public static float FadeOutTime; 

    float timer;

    void Awake()
    {
        if (levelFader != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        levelFader = this;
        canvasGroup = GetComponent<CanvasGroup>();
        FadeOutTime = fadeOutTime;
        //StartCoroutine(FadeInOut());
    }

    /*void FixedUpdate()
    {
        timer += Time.deltaTime;
    }*/

    public void Fade()
    {
        StartCoroutine(FadeInOut());
    }
    
    IEnumerator FadeInOut()
    {
        yield return StartCoroutine(FadeOut(fadeOutTime));
        yield return StartCoroutine(DarkTime(darkTime));
        yield return StartCoroutine(FadeIn(fadeInTime));
    }

    public IEnumerator FadeOut(float time)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }
    
    public IEnumerator FadeIn(float time)
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }
    
    public IEnumerator DarkTime(float time)
    {
        timer = 0f;
        while (timer < darkTime)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = 1;
            yield return null;
        }
    }
}
