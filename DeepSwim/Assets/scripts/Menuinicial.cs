using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject menuInicial;
    public GameObject creditos;
    public GameObject juegoSelect;
    public GameObject tutoguia;



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

    public void Jugar1()
    {
        SceneManager.LoadScene("EscenaUno");
    }
    public void Jugar2()
    {
        SceneManager.LoadScene("EscenaDos");
    }

    public void Guia()
    {
        juegoSelect.SetActive(false);
        tutoguia.SetActive(true);
    }
    public void OpenSelect()
    {
        juegoSelect.SetActive(true);
        tutoguia.SetActive(false);
    }



    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
    public void Multijugador()
    {
        SceneManager.LoadScene("Multijugador");
    }
}