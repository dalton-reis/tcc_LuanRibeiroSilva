using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class DetectTrackable : MonoBehaviour, ITrackableEventHandler
{

    // Use this for initialization
    void Start()
    {
        GetComponent<TrackableBehaviour>().RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
			GameObject.Find("Signal"+gameObject.name[gameObject.name.Length-1])
				.GetComponent<Animator>().SetTrigger("Letter"+gameObject.name[gameObject.name.Length-1]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
