using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public TMP_Text puntosText;
    public GameObject gameOverPanel;

    public void MostrarGameOver()
    {
        gameOverPanel.SetActive(true);
        puntosText.text = "Puntos: " + GameManager.instance.puntos;
    }


    public void ReiniciarJuego()
    {
        GameManager.instance.ReiniciarJuego(); // Reinicia los puntos
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena
    }


    public void IRaMENU()
    {
        Debug.Log("Fuiste al menú, amiga ");
        SceneManager.LoadScene("Menu");
    }

}
