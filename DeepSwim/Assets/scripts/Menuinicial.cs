using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuinicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Multijugador()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

    // Esto se llama cuando la nueva escena termine de cargar
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActualizarReferenciasUI();
        }

        // quitamos el evento para no acumularlo
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
