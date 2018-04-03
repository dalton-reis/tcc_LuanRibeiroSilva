using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{
    public GameObject text;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.name[gameObject.name.Length-1].Equals(other.gameObject.name[other.gameObject.name.Length-1]))
            text.GetComponent<TextMesh>().color = Color.blue;
        else
            text.GetComponent<TextMesh>().color = Color.red;
    }

    private void OnTriggerExit(Collider other)
    {
        text.GetComponent<TextMesh>().color = new Color32(0, 212, 1, 255);
    }

}
