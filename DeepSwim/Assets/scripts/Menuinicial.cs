using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject menuInicial;
    public GameObject creditos;

    public void OpenCreditosPanel()
    { 
    menuInicial.SetActive(false);
        creditos.SetActive(true);
    }

    public void OpenMenuPanel()
    {
        menuInicial.SetActive(true);
        creditos.SetActive(false);
    }
    public void Jugar()
    {
        SceneManager.LoadScene("EscenaUno");
    }

    public void Multijugador()
    {
        SceneManager.LoadScene("Multijugador");
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}