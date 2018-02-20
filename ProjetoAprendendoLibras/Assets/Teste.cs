using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{

    private Animator animacao;

    void Start()
    {
        animacao = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			animacao.SetTrigger("Next");
        }
    }
}
