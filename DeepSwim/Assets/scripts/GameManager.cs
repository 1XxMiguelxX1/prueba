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

    /// /////////////////////


    //Reiniciar cuando muere

    //public void RestartScene()
    //{
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    puntos = 0;
    //    SceneManager.LoadScene(currentSceneName);
    //}

 




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

    private void Update()
    {
        puntosText = GameObject.Find("puntosText").GetComponent<TMP_Text>(); //"
    }

    ///Audio
    void Start()
    {
        if (audioSource == null)
        {
        audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayPointSound()
    {
        audioSource.PlayOneShot(pointSound);
    }






}
