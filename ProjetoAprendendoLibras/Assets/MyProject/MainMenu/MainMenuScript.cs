﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class MainMenuScript : MonoBehaviour
{

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
    }

    public void BackButton()
    {
        Application.Quit();
    }

    public void LoadGameARScene()
    {
        StartCoroutine(LoadScene("GameARScene"));
        //SceneManager.LoadScene("GameAR");
    }

    public void LoadLearnSignalsScene()
    {
        StartCoroutine(LoadScene("LearnSignalsScene"));
        //SceneManager.LoadScene("LearnSignalsScene");
    }

    public void LoadGameVRScene()
    {
        StartCoroutine(LoadScene("GameVRScene"));
        //SceneManager.LoadScene("GameVRScene");
    }

    IEnumerator LoadScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }
}
