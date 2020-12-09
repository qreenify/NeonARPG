using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] FlashImage _flashImage = null;
    [SerializeField] Color _newColor = Color.red;
    public float FlashDuration; 
    IEnumerator StopFlash(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _flashImage.StopFlashLoop();;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _flashImage.Flash(FlashDuration, 0, 1);
           StartCoroutine(StopFlash(FlashDuration)); 
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            _flashImage.StopFlashLoop();
        }
    }

}
