using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEngine.UI;

public class GameARScript : MonoBehaviour
{

    public GameObject menuObject;
    public GameObject markersObject;
    public Text finishText;
    public Text gameOverText;
    public Text errorsText;
    public Sprite spriteUncheck;

    string[] letters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                        "U", "V", "W", "X", "Y", "Z"};

    string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    string[] currentSignals;
    string[] aleatorySignals;

    GameObject[] markersSignals;
    GameObject[] markersLetters;
    GameObject[] textResults;
    GameObject[] imagesTextResult;

    int errors;
    int totalErrors = 5;

    void Start()
    {
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
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            BackButton();
        }
    }

    public void BackButton()
    {
        VuforiaBehaviour.Instance.enabled = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    void Init()
    {
        if (VuforiaRuntime.Instance.HasInitialized)
            VuforiaBehaviour.Instance.enabled = true;
        else
            StartCoroutine(StartVuforia());

        menuObject.SetActive(false);
        markersObject.SetActive(true);

        markersSignals = GameObject.FindGameObjectsWithTag("MarkerSignal");
        markersLetters = GameObject.FindGameObjectsWithTag("MarkerLetter");
        textResults = GameObject.FindGameObjectsWithTag("TextResult");
        imagesTextResult = GameObject.FindGameObjectsWithTag("ImageTextResult");

        CreateMarkers();

        errors = 0;
        errorsText.text = "Erros: " + errors + "/" + totalErrors;
    }

    void RestartGame()
    {
        VuforiaBehaviour.Instance.enabled = false;

        for (int i = 0; i < imagesTextResult.Length; i++)
        {
            imagesTextResult[i].GetComponent<UnityEngine.UI.Image>().sprite = spriteUncheck;
            markersSignals[i].name = markersSignals[i].name.Remove(markersSignals[i].name.Length - 1);
            markersSignals[i].GetComponentInChildren<Animator>().Rebind();
            markersLetters[i].name = markersLetters[i].name.Remove(markersLetters[i].name.Length - 1);
            markersLetters[i].GetComponentInChildren<TextMesh>().color = new Color32(0, 212, 1, 255);
            imagesTextResult[i].name = imagesTextResult[i].name.Remove(imagesTextResult[i].name.Length - 1);
        }

        finishText.enabled = false;
        gameOverText.enabled = false;

        menuObject.SetActive(true);
        markersObject.SetActive(false);
    }

    public void GameNumbersButton()
    {
        currentSignals = numbers;
        Init();
    }

    public void GameLettersButton()
    {
        currentSignals = letters;
        Init();
    }

    void CreateMarkers()
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
            markersSignals[i].name += signal;
            markersLetters[i].name += signal;
            textResults[i].GetComponent<Text>().text = signal;
            imagesTextResult[i].name += signal;
        }
    }

    public void CheckFinishGame(string signal)
    {
        aleatorySignals = aleatorySignals.Where(x => x != signal).ToArray();

        if (aleatorySignals.Length == 0)
        {
            finishText.enabled = true;
            Invoke("RestartGame", 3);
        }
    }

    public void CheckErrors()
    {
        errors++;

        if (errors <= totalErrors)
        {
            errorsText.text = "Erros: " + errors + "/" + totalErrors;

            if (errors == totalErrors)
            {
                gameOverText.enabled = true;
                Invoke("RestartGame", 3);
            }
        }
    }

}
