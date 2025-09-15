using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{
    public TMP_Text puntosText;
    public TMP_Text metrosText;
    public GameObject gameOverPanel;
    public metraje distanciaRecorrida; // referencia al script que controla metros
    public Button botonRecompensa;


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
        AdsController.instance.ShowInterstitialAd();
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

    public void OnBotonRecompensaClick()
    {
        if (botonRecompensa != null)
        {
            botonRecompensa.interactable = false;
        }

        AdsController.instance.ShowRewardedAd((Reward reward) =>
        {
            // El jugador vio el anuncio, ¡a revivir!

            // 1. Damos la vida extra.
            GameManager.instance.GanarVida();

            // 2. Ocultamos este panel de Game Over.
            gameOverPanel.SetActive(false);

            // 3. Reanudamos el contador de metros.
            if (distanciaRecorrida != null)
            {
                distanciaRecorrida.Reanudar();
            }

            // 4. Buscamos al jugador y llamamos a su método para revivir.
            playercontroler jugador = FindObjectOfType<playercontroler>();
            if (jugador != null)
            {
                jugador.Revivir();
            }
        });

    }
}
