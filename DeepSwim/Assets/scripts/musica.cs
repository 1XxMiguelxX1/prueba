using UnityEngine;
using System.Collections.Generic;

public class musica : MonoBehaviour
{
    public static musica instance;

    // AudioSource para la música
    public AudioSource audioSource;

    // Lista de canciones disponibles
    public List<AudioClip> canciones;

    [System.Serializable]
    public class CancionNivel
    {
        public string nombreEscena;
        public int indiceCancion; // Índice en la lista "canciones"
    }

    // Configuración de qué canción tocar en cada nivel
    public List<CancionNivel> cancionesPorNivel;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    // Reproduce la canción correspondiente al nivel actual
    public void ReproducirCancionActual()
    {
        string escena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int indice = -1;

        foreach (var cn in cancionesPorNivel)
        {
            if (cn.nombreEscena == escena)
            {
                indice = cn.indiceCancion;
                break;
            }
        }

        if (indice >= 0 && indice < canciones.Count)
        {
            audioSource.clip = canciones[indice];
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se encontró canción para esta escena o índice inválido.");
        }
    }

    public void Pausar()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void Reanudar()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }

    // Opcional: reproducir una canción específica por índice
    public void ReproducirPorIndice(int indice)
    {
        if (indice >= 0 && indice < canciones.Count)
        {
            audioSource.clip = canciones[indice];
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Índice de canción inválido.");
        }
    }
}
