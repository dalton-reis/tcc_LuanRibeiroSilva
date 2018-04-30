using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ProgressBar;
using Vuforia;

public class GameVRScript : MonoBehaviour
{

    const float MaxTimer = 25f;
    const int MaxErrors = 2;

    public GameObject progress;
    public GameObject menuObject;
    public GameObject gameVRObject;
    public Button startButton;
    public Text aleatorySignalText;
    public Text secondText;
    public Text numberErrorsText;
    public Text finishText;
    public Text gameOverText;

    GameObject objectClick;
    ProgressRadialBehaviour scriptProgress;
    GameObject[] hands;

    string[] letters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                        "U", "V", "W", "X", "Y", "Z"};

    string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    string[] currentSignals;
    string[] aleatorySignals;
    string aleatorySignal;

    float timer;
    bool beginTimer = false;

    int errors;

    void Start()
    {
        scriptProgress = progress.GetComponentInChildren<ProgressRadialBehaviour>();

        VuforiaBehaviour.Instance.enabled = false;
        StartCoroutine(LoadVR());
    }

    IEnumerator LoadVR()
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName("cardboard");
        yield return null;
        UnityEngine.XR.XRSettings.enabled = true;
    }

    IEnumerator CloseVR()
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName("none");
        yield return null;
        UnityEngine.XR.XRSettings.enabled = false;
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }

        if (beginTimer)
        {
            timer -= Time.deltaTime;
            if (timer >= 0)
            {
                secondText.text = timer.ToString("0");
            }
            else
            {
                GameOver();
            }
        }
    }

    public void BackButton()
    {
        StartCoroutine(CloseVR());
    }

    public void StartProgress(GameObject objectClick)
    {
        this.objectClick = objectClick;
        scriptProgress.IncrementValue(100);
    }

    public void CancelProgress()
    {
        scriptProgress.Value = 0.01f;
        scriptProgress.TransitoryValue = 0;
    }

    public void ExecuteAction()
    {
        CancelProgress();
        ExecuteEvents.Execute<IPointerClickHandler>(objectClick,
            new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    void Init()
    {
        menuObject.SetActive(false);
        gameVRObject.SetActive(true);

        errors = 0;
        numberErrorsText.text = errors + "/" + MaxErrors;

        timer = MaxTimer;
        beginTimer = true;

        hands = GameObject.FindGameObjectsWithTag("Hand");

        CreateSignals();
    }

    public void LearnNumbersButton()
    {
        currentSignals = numbers;
        Init();
    }

    public void LearnLettersButton()
    {
        currentSignals = letters;
        Init();
    }

    void CreateSignals()
    {
        aleatorySignals = new string[] { "", "", "", "" };

        for (int i = 0; i < 4; i++)
        {
            string signal = currentSignals[Random.Range(0, currentSignals.Length)];

            while (System.Array.IndexOf(aleatorySignals, signal) != -1)
            {
                signal = currentSignals[Random.Range(0, currentSignals.Length)];
            }

            aleatorySignals[i] = signal;
            hands[i].name += signal;
            //hands[i].GetComponentInChildren<Animator>().Play("Signal" + signal);
        }

        ReloadSignalButton();

        aleatorySignal = aleatorySignals[Random.Range(0, aleatorySignals.Length)];
        aleatorySignalText.text = aleatorySignal;
    }

    public void CheckChooseSignal()
    {
        if (aleatorySignal.Equals(objectClick.name[objectClick.name.Length - 1].ToString()))
        {
            FinishGame();
        }
        else
        {
            errors++;
            numberErrorsText.text = errors + "/" + MaxErrors;

            if (errors == 2)
            {
                GameOver();
            }
        }
    }

    void ShowHideHands(bool show)
    {
        for (int i = 0; i < hands.Length; i++)
        {
            hands[i].GetComponentInChildren<Animator>().Rebind();
            hands[i].SetActive(show);
        }
    }

    void FinishGame()
    {
        beginTimer = false;
        ShowHideHands(false);
        finishText.gameObject.SetActive(true);
        Invoke("RestartGame", 3);
    }

    void GameOver()
    {
        beginTimer = false;
        ShowHideHands(false);
        gameOverText.gameObject.SetActive(true);
        Invoke("RestartGame", 3);
    }

    public void RestartGame()
    {
        ShowHideHands(true);
        gameOverText.gameObject.SetActive(false);
        finishText.gameObject.SetActive(false);
        gameVRObject.SetActive(false);
        menuObject.SetActive(true);
    }

    public void ReloadSignalButton()
    {
        for (int i = 0; i < hands.Length; i++)
        {
            char signal = hands[i].name[hands[i].name.Length - 1];
            Animator animator = hands[i].GetComponentInChildren<Animator>();
            animator.Rebind();
            animator.Play("Signal" + signal);
        }
    }
}
