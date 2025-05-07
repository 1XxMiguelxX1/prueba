using System.Collections;
using UnityEngine;

public class MuerteController : MonoBehaviour
{
    // Duraci�n de la animaci�n de muerte (aj�stala seg�n tu animaci�n)
    public float duracionAnimacion = 1.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Intenta obtener el componente Animator del objeto colisionante
            Animator anim = collision.gameObject.GetComponent<Animator>();

            if (anim != null)
            {
                // Activa el trigger de la animaci�n de muerte (aseg�rate de que el par�metro se llame "muerte")
                anim.SetTrigger("muerte");
                // Inicia la corrutina para destruir el objeto despu�s de la animaci�n
                StartCoroutine(DestruirDespuesDeAnimacion(collision.gameObject));
            }
            else
            {
                // Si no se encuentra Animator, se destruye el objeto de inmediato
                Destroy(collision.gameObject);
                Debug.Log("Game Over");
                FindAnyObjectByType<GameOver>().MostrarGameOver();
            }
        }
    }

    private IEnumerator DestruirDespuesDeAnimacion(GameObject objeto)
    {
        // Espera la duraci�n de la animaci�n antes de destruir el objeto
        yield return new WaitForSeconds(duracionAnimacion);
        Destroy(objeto);
        Debug.Log("Game Over");
        FindAnyObjectByType<GameOver>().MostrarGameOver();
    }
}