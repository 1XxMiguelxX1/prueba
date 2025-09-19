using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI y Puntos")]
    public TMP_Text puntosText;
    public int puntos = 0;

    [Header("Audio")]
    public AudioSource audioSource;   // Para efectos
    public AudioClip pointSound;
    public AudioClip muerteInstantaneaSound;
    public AudioClip vidaSound;
    public AudioClip dañoSound;

    [Header("Vidas")]
    public int vidas = 3;
    public GameObject[] coeurUI;
    public int recompensasVistas = 0;

    private bool partidaPausada = false;

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

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // Inicia música de fondo para la escena actual
        musica.instance?.ReproducirCancionActual();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ActualizarReferenciasUI();
        ReiniciarJuego();
    }

    // ----------------- PUNTOS -----------------
    public void SumarPunto()
    {
        puntos++;
        PlayPointSound();
        ActualizarPuntosUI();
    }

    void ActualizarPuntosUI()
    {
        if (puntosText != null)
            puntosText.text = puntos.ToString();
    }

    // ----------------- VIDAS -----------------
    public void PerderVida()
    {
        playercontroler jugador = FindObjectOfType<playercontroler>();
        if (jugador != null && jugador.estaMuerto) return;

        vidas--;
        ActualizarVidasUI();
        PlayDañoSound();

        if (vidas <= 0)
        {
            PausarMusica();
            if (jugador != null)
                jugador.Morir();
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

    public void Cmuere()
    {
        playercontroler jugador = FindObjectOfType<playercontroler>();
        if (jugador != null && !jugador.estaMuerto)
        {
            vidas = 0;
            ActualizarVidasUI();
            PlayMuerteInstantaneaSound();
            PausarMusica();
            jugador.Morir();
        }
    }

    public void IncrementarRecompensasVistas()
    {
        recompensasVistas++;
    }

    // ----------------- REINICIO -----------------
    public void ReiniciarJuego()
    {
        puntos = 0;
        vidas = 3;
        recompensasVistas = 0;
        partidaPausada = false;

        ActualizarPuntosUI();
        ActualizarVidasUI();

        // Reinicia música
        musica.instance?.ReproducirCancionActual();
    }

    public void ActualizarReferenciasUI()
    {
        puntosText = GameObject.Find("puntosText")?.GetComponent<TMP_Text>();
        coeurUI = GameObject.FindGameObjectsWithTag("VidasUI");

        if (puntosText == null)
            Debug.LogWarning("No se encontró puntosText en la escena.");

        ActualizarPuntosUI();
        ActualizarVidasUI();
    }

    // ----------------- AUDIO -----------------
    void PlayPointSound() { if (pointSound != null) audioSource.PlayOneShot(pointSound); }
    void PlayVidaSound() { if (vidaSound != null) audioSource.PlayOneShot(vidaSound); }
    public void PlayDañoSound() { if (dañoSound != null) audioSource.PlayOneShot(dañoSound); }
    public void PlayMuerteInstantaneaSound() { if (muerteInstantaneaSound != null) audioSource.PlayOneShot(muerteInstantaneaSound); }

    public void PausarMusica()
    {
        musica.instance?.Pausar();
        partidaPausada = true;
    }

    public void ReanudarMusica()
    {
        musica.instance?.Reanudar();
        partidaPausada = false;
    }
}
