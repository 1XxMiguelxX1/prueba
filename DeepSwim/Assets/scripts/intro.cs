using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI; // Necesario para Image
using System.Collections; // Necesario para Coroutine

public class VideoIntroController : MonoBehaviour
{
    [Header("Componentes")]
    public VideoPlayer videoPlayer;
    public Image fadePanel; // ¡Arrastra el FadePanel de UI aquí!

    [Header("Configuración de Carga")]
    public string nextSceneName = "MainMenuScene";
    [Range(0.5f, 3f)]
    public float fadeDuration = 1.5f; // Duración de la atenuación (segundos)

    private bool isLoading = false; // Bandera para evitar llamadas múltiples

    void Start()
    {
        if (videoPlayer == null || fadePanel == null)
        {
            Debug.LogError("Componentes requeridos no asignados. ¡Revisa el Inspector!");
            return;
        }

        // Suscribirse al evento de finalización del video
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();

        // Opcional: Para el caso de videos cortos, puedes iniciar una Corrutina
        // que monitorice el tiempo para empezar la carga asíncrona ANTES del final.
        //float startLoadTime = (float)videoPlayer.clip.length - fadeDuration - 0.5f; // 0.5s de buffer
        //StartCoroutine(MonitorVideoProgress(startLoadTime));
    }

    /// <summary>
    /// Se invoca cuando el video llega al final.
    /// </summary>
    void OnVideoFinished(VideoPlayer vp)
    {
        if (!isLoading)
        {
            isLoading = true;
            // Iniciar la transición visual y la carga de escena
            StartCoroutine(LoadNextSceneWithFade());
        }
    }

    /// <summary>
    /// Corrutina que gestiona el fade out y la carga asíncrona.
    /// </summary>
    IEnumerator LoadNextSceneWithFade()
    {
        // 1. Activar el panel de Fade
        fadePanel.gameObject.SetActive(true);
        float timer = 0f;

        // 2. Cargar la escena de destino en segundo plano
        // Usaremos 'allowSceneActivation = false' para tener control total
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        asyncLoad.allowSceneActivation = false;

        // 3. Animación de Fade Out (a negro)
        Color finalColor = Color.black; // El color negro tiene A=1 (opaco)
        Color startColor = fadePanel.color; // Ya debe ser negro con A=0

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            // Interpolación lineal del color (solo afecta la transparencia)
            startColor.a = Mathf.Lerp(0f, 1f, t);
            fadePanel.color = startColor;

            yield return null; // Esperar un frame
        }

        // 4. Asegurar que el fade sea 100% opaco
        fadePanel.color = finalColor;

        // 5. Permitir la activación de la escena (¡Cambio!)
        asyncLoad.allowSceneActivation = true;
    }
}