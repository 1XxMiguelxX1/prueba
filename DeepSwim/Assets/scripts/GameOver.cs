using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public TMP_Text puntosText;
    public TMP_Text metrosText;  // NUEVO: texto para metros
    public GameObject gameOverPanel;

    public metraje distanciaRecorrida; // referencia al script que controla metros

    public void MostrarGameOver()
    {
        gameOverPanel.SetActive(true);

        puntosText.text = "Puntos: " + GameManager.instance.puntos;

        // Detenemos el contador de metros y mostramos la distancia final
        if (distanciaRecorrida != null)
        {
            distanciaRecorrida.Detener();
            metrosText.text = "Metros: " + distanciaRecorrida.GetDistancia();
        }
    }

    public void ReiniciarJuego()
    {
        GameManager.instance.ReiniciarJuego();
        if (distanciaRecorrida != null)
        {
            distanciaRecorrida.Reiniciar();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IRaMENU()
    {
        Debug.Log("Fuiste al menú, amiga ");
        SceneManager.LoadScene("Menu");
    }
}
