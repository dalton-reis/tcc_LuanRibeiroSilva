using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadGameARScene()
    {
        SceneManager.LoadScene("GameAR");
    }

    public void LoadLearnSignalsScene()
    {
        SceneManager.LoadScene("LearnSignalsScene");
    }
}
