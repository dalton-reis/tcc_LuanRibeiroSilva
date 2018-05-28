using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class DetectTrackable : MonoBehaviour, ITrackableEventHandler
{

    void Start()
    {
        GetComponent<TrackableBehaviour>().RegisterTrackableEventHandler(this);
    }

    void Update()
    {

    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            char lastLetter = gameObject.name[gameObject.name.Length - 1];

            GetComponentInChildren<Animator>().Rebind();
            GetComponentInChildren<Animator>().Play("Signal" + lastLetter);
        }
    }
}
