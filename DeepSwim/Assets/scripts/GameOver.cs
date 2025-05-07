using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//public class GameOver : MonoBehaviour
//{

//    public TMP_Text puntosText;
//    public GameObject gameOverPanel;

//    public void MostrarGameOver ()
//    {
//        gameOverPanel.SetActive(true);
//        puntosText.text = (("Puntos: ") + FindAnyObjectByType<GameManager>().puntos).ToString ();
//    }
//}

public class GameOver : MonoBehaviour
{
    public TMP_Text puntosText;
    public GameObject gameOverPanel;

    public void MostrarGameOver()
    {
        gameOverPanel.SetActive(true);
        puntosText.text = "Puntos: " + GameManager.instance.puntos;
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IRaMENU()
    {
        Debug.Log("Fuiste al menú, amiga ");
        SceneManager.LoadScene("Menu");
    }

}
