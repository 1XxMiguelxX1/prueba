using System.Collections;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    public float duracionAnimacion = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Kill(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Kill(other.gameObject);
        }
    }

    private void Kill(GameObject player)
    {
        var pc = player.GetComponent<playercontroler>();
        if (pc == null || pc.estaMuerto) return;

        Animator anim = player.GetComponent<Animator>();
        if (anim != null)
        {
            pc.estaMuerto = true;
            anim.SetTrigger("muerte");

            // OPCIÓN 2: poner vidas a 0 y actualizar de golpe
            GameManager.instance.vidas = 0;
            GameManager.instance.ActualizarVidasUI();

            StartCoroutine(DestruirDespuesDeAnimacion(player));
        }
        else
        {
            player.SetActive(false);

            GameManager.instance.vidas = 0;
            GameManager.instance.ActualizarVidasUI();

            FindAnyObjectByType<GameOver>()?.MostrarGameOver();
        }
    }

    private IEnumerator DestruirDespuesDeAnimacion(GameObject player)
    {
        yield return new WaitForSeconds(duracionAnimacion);

        Destroy(player);

        var gameOver = FindAnyObjectByType<GameOver>();
        if (gameOver != null)
        {
            gameOver.MostrarGameOver();
        }
        else
        {
            Debug.LogError("No se encontró ningún GameOver en la escena.");
        }
    }
}
