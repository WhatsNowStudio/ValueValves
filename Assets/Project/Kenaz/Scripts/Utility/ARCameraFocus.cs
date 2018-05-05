#if VUFORIA_ANDROID_SETTINGS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/*
This script is from:
http://www.chinaar.com/Vuforia/1988.html
*/

public class ARCameraFocus : MonoBehaviour
{
    //private string label;
    private float touchDuration = 0f;
    private Touch touch;

    // Use this for initialization
    void Start()
    {

        CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.touchCount > 0f)
        {
            touchDuration += Time.deltaTime; touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended && touchDuration < 0.2f)
            {

                StartCoroutine("SingleOrDouble");
            }
        }
        else
        {
            touchDuration = 0f;
        }
    }

    IEnumerator SingleOrDouble()
    {

        yield return new WaitForSeconds(0.3f);

        if (touch.tapCount == 1)
        {
            Debug.Log("Single");
            OnSingleTapped();
        }
        else if (touch.tapCount == 2)
        {
            StopCoroutine("SingleOrDouble");
            Debug.Log("Double");
            OnDoubleTapped();
        }
    }

    private void OnSingleTapped()
    {
        TriggerAutoFocus();
        //label = "Tap the Screen!"; 
    }

    private void OnDoubleTapped()
    {
        //label = "Double Tap the Screen!"; 
    }

    public void TriggerAutoFocus()
    {
        StartCoroutine(TriggerAutoFocusAndEnableContinuousFocusIfSet());
    }

    private IEnumerator TriggerAutoFocusAndEnableContinuousFocusIfSet()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
        yield return new WaitForSeconds(1.0f);
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

    }
}

#endif