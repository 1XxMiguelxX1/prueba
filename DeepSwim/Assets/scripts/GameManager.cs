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
    public AudioClip pointSound;
    public AudioClip muerteInstantaneaSound;

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
        // Solo en la escena de juego
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
            Debug.LogWarning("No se encontraron corazones en la escena.");
        if (puntosText == null)
            Debug.LogWarning("No se encontró puntosText en la escena.");

        ActualizarPuntosUI();
        ActualizarVidasUI();
    }

    private void Update()
    {
       // puntosText = GameObject.Find("puntosText").GetComponent<TMP_Text>();
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
        recompensasVistas = 0; // 🔄 Reiniciamos el contador
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
            // Buscamos al jugador y le decimos que inicie la secuencia de muerte
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
            vidas = 0; // Ponemos las vidas a 0
            ActualizarVidasUI();
            PlayMuerteInstantaneaSound(); // O el sonido de daño que prefieras
            jugador.Morir(); // Le decimos al jugador que inicie su secuencia de muerte
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

}