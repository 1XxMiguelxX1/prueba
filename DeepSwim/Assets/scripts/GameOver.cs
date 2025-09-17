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
        // ... (código para mostrar puntos y metros)

        // --- LÓGICA DEL LÍMITE DE RECOMPENSAS ---
        // Verificamos si el jugador ha visto menos de 3 recompensas
        Debug.Log("Mostrando Game Over. Recompensas vistas hasta ahora: " + GameManager.instance.recompensasVistas);

        if (GameManager.instance.recompensasVistas < 3)
        {
            // Si es así, mostramos el botón y lo hacemos interactuable
            botonRecompensa.gameObject.SetActive(true); 
            botonRecompensa.interactable = true;
            Debug.Log($"Recompensas vistas: {GameManager.instance.recompensasVistas}. Mostrando botón.");
        }
        else
        {
            // Si ya vio 3, ocultamos el botón por completo
            botonRecompensa.gameObject.SetActive(false);
            Debug.Log($"Límite de recompensas alcanzado. Ocultando botón.");
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


    public void OnbotonRecompensaClick()
    {
        if (botonRecompensa != null)
        {
            botonRecompensa.interactable = false;
        }

        AdsController.instance.ShowRewardedAd((Reward reward) =>
        {
            // Le decimos al GameManager que registre que ya se vio una recompensa.
            GameManager.instance.IncrementarRecompensasVistas();
            // ---------------------------------------------------------

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
