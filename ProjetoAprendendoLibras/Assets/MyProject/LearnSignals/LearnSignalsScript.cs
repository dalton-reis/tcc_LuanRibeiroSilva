using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LearnSignalsScript : MonoBehaviour
{

    public Text uppercaseLetterText;
    public Text lowerLetterText;

    Transform transformHand;
    Animator animatorHand;

    string[] letters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                        "U", "V", "W", "X", "Y", "Z"};

    int positionCurrentSignal = -1;

    float f_lastX = 0.0f;
    float f_difX = 0.5f;

    void Start()
    {
        GameObject hand = GameObject.Find("Hand");
        transformHand = hand.GetComponent<Transform>();
        animatorHand = GameObject.Find("Hand").GetComponentInChildren<Animator>();

        Invoke("NextSignalButton", 0.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
 
            if (touch.phase == TouchPhase.Moved)
            {
                transformHand.Rotate(0f, touch.deltaPosition.x * 0.3f, 0f);
            }
 
        }
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    public void NextSignalButton()
    {
        if (positionCurrentSignal != letters.Length - 1)
        {
            transformHand.rotation = Quaternion.Euler(0, -80, 0);
            positionCurrentSignal++;
            PlayAnimationSignal();
        }
    }

    public void PreviusSignalButton()
    {
        if (positionCurrentSignal != 0)
        {
            transformHand.rotation = Quaternion.Euler(0, -80, 0);
            positionCurrentSignal--;
            PlayAnimationSignal();
        }
    }

    void PlayAnimationSignal()
    {
        string letter = letters[positionCurrentSignal];
        uppercaseLetterText.text = letter;
        lowerLetterText.text = letter.ToLower();
        animatorHand.Play("Letter" + letter);
    }

    public void ReloadSignalButton()
    {
        animatorHand.Play("Initial");
        Invoke("PlayAnimationSignal", 0.1f);
    }
}
