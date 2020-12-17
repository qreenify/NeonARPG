using System;
using System.Collections;
using UnityEngine;

public class LevelFader : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [Range(0.1f, 5.0f)]
    [SerializeField] float fadeOutTime = 0.5f;
    [Range(0.1f, 5.0f)]
    [SerializeField] float darkTime = 1f;
    [Range(0.1f, 5.0f)]
    [SerializeField] float fadeInTime = 1f;

    float timer;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        //StartCoroutine(FadeInOut());
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
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
            canvasGroup.alpha = 1;
            yield return null;
        }
    }
}
