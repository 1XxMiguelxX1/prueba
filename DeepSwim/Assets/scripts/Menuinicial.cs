using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject menuInicial;
    public GameObject creditos;
    public GameObject juegoSelect;


    public void OpenCreditosPanel()
    { 
    menuInicial.SetActive(false);
        creditos.SetActive(true);
    }

    public void OpenMenuPanel()
    {
        menuInicial.SetActive(true);
        creditos.SetActive(false);
        juegoSelect.SetActive(false);
    }


    public void juego1_2()
    {
        menuInicial.SetActive(false);
        juegoSelect.SetActive(true);
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