using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vuforia;

public class LearnSignalsScript : MonoBehaviour
{

    public Text uppercaseLetterText;
    public Text lowerLetterText;
    public Text numberText;
    public GameObject signalsObject;
    public GameObject menuObject;

    Transform transformHand;
    Animator animatorHand;

    string[] letters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                        "U", "V", "W", "X", "Y", "Z"};

    string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    string[] currentSignals;

    int positionCurrentSignal;

    void Start()
    {
        VuforiaBehaviour.Instance.enabled = false;
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

    void Init()
    {
        menuObject.SetActive(false);
        signalsObject.SetActive(true);

        GameObject hand = GameObject.Find("Hand");
        transformHand = hand.GetComponent<Transform>();
        animatorHand = GameObject.Find("Hand").GetComponentInChildren<Animator>();

        positionCurrentSignal = -1;
        NextSignalButton();   
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    public void MenuButton()
    {
        animatorHand.Rebind();
        menuObject.SetActive(true);
        signalsObject.SetActive(false);
        numberText.enabled = false;
        uppercaseLetterText.enabled = false;
        lowerLetterText.enabled = false;
    }

    public void LearnNumbersButton()
    {
        currentSignals = numbers;
        numberText.enabled = true;
        Init();
    }

    public void LearnLettersButton()
    {
        currentSignals = letters;
        uppercaseLetterText.enabled = true;
        lowerLetterText.enabled = true;
        Init();
    }

    public void NextSignalButton()
    {
        if (positionCurrentSignal != currentSignals.Length - 1)
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
        string signal = currentSignals[positionCurrentSignal];
        uppercaseLetterText.text = signal;
        lowerLetterText.text = signal.ToLower();
        numberText.text = signal;
        animatorHand.Play("Signal" + signal);
    }

    public void ReloadSignalButton()
    {
        animatorHand.Play("Initial");
        Invoke("PlayAnimationSignal", 0.1f);
    }
}
