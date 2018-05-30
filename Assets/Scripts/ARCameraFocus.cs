//#if VUFORIA_ANDROID_SETTINGS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;



public class ARCameraFocus: MonoBehaviour
{

    void Start()
    {

        CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

    }

    

  
   public void AutoFocus()
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

//#endif