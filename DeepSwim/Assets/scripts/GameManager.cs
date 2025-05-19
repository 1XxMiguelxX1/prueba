using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.VersionControl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text puntosText;
    public int puntos = 0;
    public AudioSource audioSource;
    public AudioClip pointSound;

    public int vidas = 3;
    public AudioClip vidaSound;
    public GameObject[] coeurUI;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        puntosText = GameObject.Find("puntosText").GetComponent<TMP_Text>();
    }

    ///Audio
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            ActualizarPuntosUI();
            ActualizarVidasUI();
        }
    }

    public void PlayPointSound()
    {
        if (pointSound != null)
            audioSource.PlayOneShot(pointSound);
    }

    void PlayVidaSound()
    {
        if (vidaSound != null)
            audioSource.PlayOneShot(vidaSound);
    }


    //Puntos
    public void SumarPunto()
    {
        puntos += 1; 
        PlayPointSound();
        ActualizarPuntosUI();
    }
    void ActualizarPuntosUI()
    {
        if (puntosText != null)

        {
            puntosText.text = puntos.ToString();
        }
    }
    public void ReiniciarJuego()
    {
        puntos = 0;
        vidas = 3;
        ActualizarPuntosUI();
        ActualizarVidasUI();

    }


    //VIdas
    public void PerderVida()
    {
        vidas--;
        ActualizarVidasUI();
        if (vidas <= 0)
        {
            // Matar jugador aquí o llamar a GameOver
            FindAnyObjectByType<GameOver>().MostrarGameOver();
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

    void ActualizarVidasUI()
    {
        for (int i = 0; i < coeurUI.Length; i++)
        {
            coeurUI[i].SetActive(i < vidas);
        }
    }

 
 

}
