using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectTrigger : MonoBehaviour
{
    public GameObject text;
    public Sprite spriteCheck;

    GameARScript gameARScript;

    void Start()
    {
        gameARScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameARScript>();
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag.Equals(other.tag))
        {
            return;
        }

        char signal = gameObject.name[gameObject.name.Length - 1];

        if (signal.Equals(other.gameObject.name[other.gameObject.name.Length - 1]))
        {
            text.GetComponent<TextMesh>().color = Color.blue;
            GameObject.Find("ImageTextResult" + signal).GetComponent<Image>().sprite = spriteCheck;

            gameARScript.CheckFinishGame(signal.ToString());
        }
        else
        {
            text.GetComponent<TextMesh>().color = Color.red;

            gameARScript.CheckErrors();
        }
    }

    void OnTriggerExit(Collider other)
    {
        text.GetComponent<TextMesh>().color = new Color32(0, 212, 1, 255);
    }

}
