using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
//using UnityEditor.VersionControl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text puntosText;
    public int puntos = 0;
    public AudioSource audioSource;
    public AudioSource chanson;
    public AudioClip pointSound;
    public AudioClip muerteInstantaneaSound;

    private bool partidaPausada = false;
    public int vidas = 3;
    public AudioClip vidaSound;
    public GameObject[] coeurUI;
    public int recompensasVistas = 0;


    private void Awake()

    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)

    {
        if (scene.name == "EscenaUno")

        {
            ActualizarReferenciasUI();
            ReiniciarJuego();
        }
    }


    public void ActualizarReferenciasUI()

    {
        puntosText = GameObject.Find("puntosText")?.GetComponent<TMP_Text>();
        coeurUI = GameObject.FindGameObjectsWithTag("VidasUI");

        if (coeurUI.Length == 0)
        if (puntosText == null)

            Debug.LogWarning("No se encontró puntosText en la escena.");

        ActualizarPuntosUI();
        ActualizarVidasUI();
    }

    ///Audio

    void Start()

    {
        if (audioSource == null)

        {
            audioSource = GetComponent<AudioSource>();
            // ActualizarPuntosUI();
            //ActualizarVidasUI();
        }
        if (chanson != null && !chanson.isPlaying)
        {
            chanson.Play();
        }
    }

    public void SumarPunto()

    {
        puntos += 1;
        PlayPointSound();
        ActualizarPuntosUI();
    }

    void ActualizarPuntosUI()

    {
        if (puntosText != null)
            puntosText.text = puntos.ToString();
    }

    public void ReiniciarJuego()
    {
        puntos = 0;
        vidas = 3;
        recompensasVistas = 0;
        partidaPausada = false; // También es bueno reiniciar esta variable

        // Detenemos cualquier música anterior y la volvemos a empezar.
        if (chanson != null)
        {
            chanson.Stop();
            chanson.Play();
        }
        // ----------------------

        ActualizarPuntosUI();
        ActualizarVidasUI();
    }


    public void PerderVida()

    {
        if (FindObjectOfType<playercontroler>().estaMuerto) return; // Evitar perder vidas si ya está muriendo
        vidas--;
        ActualizarVidasUI();
        PlayDañoSound();
        if (vidas <= 0)
        {
            if (chanson != null)
            {
                chanson.Pause(); // Pausa la música.
                partidaPausada = true;
            }

            playercontroler jugador = FindObjectOfType<playercontroler>();
            if (jugador != null)
            {
                jugador.Morir();
            }
        }
    }

    public void GanarVida()

    {
        if (vidas < coeurUI.Length)

        {
            vidas++;
            PlayVidaSound();
            ActualizarVidasUI();
        }

    }

    public void ActualizarVidasUI()

    {

        if (coeurUI == null || coeurUI.Length == 0)

        {
            Debug.LogWarning("coeurUI está vacío.");
            return;
        }

        for (int i = 0; i < coeurUI.Length; i++)
        {
            if (coeurUI[i] != null)
                coeurUI[i].SetActive(i < vidas);
        }
    }

    public void IncrementarRecompensasVistas()

    {
        recompensasVistas++;
    }

    public void Cmuere()
    {
        playercontroler jugador = FindObjectOfType<playercontroler>();
        if (jugador != null && !jugador.estaMuerto)
        {
            vidas = 0;
            ActualizarVidasUI();
            PlayMuerteInstantaneaSound();

            if (chanson != null)
            {
                chanson.Pause(); // Pausa la música.
                partidaPausada = true;
            }

            jugador.Morir();
        }
}

    void PlayPointSound()

    {
        if (pointSound != null)
            audioSource.PlayOneShot(pointSound);
    }

    void PlayVidaSound()

    {
        if (vidaSound != null)
           audioSource.PlayOneShot(vidaSound);
    }

    public AudioClip dañoSound;

    public void PlayDañoSound()

    {
        if (dañoSound != null)
            audioSource.PlayOneShot(dañoSound);
    }

    public void PlayMuerteInstantaneaSound()

    {
        if (muerteInstantaneaSound != null)
            audioSource.PlayOneShot(muerteInstantaneaSound);
    }

    public void ReanudarMusica()
    {
        // Método seguro para reanudar la música solo si fue pausada por un Game Over.
        if (partidaPausada && chanson != null)
        {
            chanson.Play(); // "Play" en un audio pausado lo reanuda.
            partidaPausada = false;
        }
    }


}