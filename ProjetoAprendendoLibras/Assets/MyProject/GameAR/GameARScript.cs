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
    public Text loadingText;
    public Button gameNumbersButton;
    public Button gameLettersButton;

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

    IEnumerator Init()
    {
        loadingText.gameObject.SetActive(true);
        gameLettersButton.gameObject.SetActive(false);
        gameNumbersButton.gameObject.SetActive(false);
        yield return null;

        if (VuforiaRuntime.Instance.HasInitialized)
        {
            VuforiaBehaviour.Instance.enabled = true;
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
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
        for (int i = 0; i < 4; i++)
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
        gameLettersButton.gameObject.SetActive(true);
        gameNumbersButton.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(false);
        
        VuforiaBehaviour.Instance.enabled = false;
    }

    public void GameNumbersButton()
    {
        currentSignals = numbers;
        StartCoroutine("Init");
    }

    public void GameLettersButton()
    {
        currentSignals = letters;
        StartCoroutine("Init");
    }

    void CreateMarkers()
    {
        aleatorySignals = new string[] { "", "", "", "" };
        int[] positionMarkers = { 0, 1, 2, 3 };

        for (int i = 0; i < 4; i++)
        {
            string signal = currentSignals[Random.Range(0, currentSignals.Length)];

            while (System.Array.IndexOf(aleatorySignals, signal) != -1)
            {
                signal = currentSignals[Random.Range(0, currentSignals.Length)];
            }

            aleatorySignals[i] = signal;
            markersSignals[i].name += signal;
            textResults[i].GetComponent<Text>().text = signal;
            imagesTextResult[i].name += signal;

            int position = positionMarkers[Random.Range(0, positionMarkers.Length)];
            positionMarkers = positionMarkers.ToList().Where(x => x != position).ToArray();
            markersLetters[position].name += signal;
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
