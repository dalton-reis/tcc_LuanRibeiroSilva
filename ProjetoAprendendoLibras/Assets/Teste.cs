using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{

    public string nomeAnimacao;
    private Animator animacao;

    void Start()
    {
        animacao = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			//animacao.SetTrigger("Next");
            animacao.Play(nomeAnimacao);
        }
    }
}
