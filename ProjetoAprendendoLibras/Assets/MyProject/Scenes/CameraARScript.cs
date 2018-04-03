using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraARScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartVuforia());

        
    }

    IEnumerator StartVuforia()
    {
        VuforiaRuntime.Instance.InitVuforia();
        while (!VuforiaRuntime.Instance.HasInitialized)
            yield return null;

        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        GetComponent<VuforiaBehaviour>().enabled = true;
    }

    void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
