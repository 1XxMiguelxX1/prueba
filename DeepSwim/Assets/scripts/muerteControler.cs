using System.Collections;
using UnityEngine;

public class MuerteController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playercontroler pc = collision.gameObject.GetComponent<playercontroler>();
            if (pc == null || pc.estaMuerto || pc.esInvencible) return;

            GameManager.instance.PerderVida();
            // Ya no hay 'Destroy()' para el jugador. El obstáculo sí se puede destruir a sí mismo.
            Destroy(gameObject);
        }
    }
}











/*
 * notas
 * 
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Intenta obtener el componente Animator del objeto colisionante
            Animator anim = collision.gameObject.GetComponent<Animator>();

            if (anim != null)
            {
                collision.gameObject.GetComponent<playercontroler>().estaMuerto = true; // ← Esto desactiva el control
                // Activa el trigger de la animación de muerte (asegúrate de que el parámetro se llame "muerte")
                anim.SetTrigger("muerte");
                // Inicia la corrutina para destruir el objeto después de la animación
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
        // Espera la duración de la animación antes de destruir el objeto
        yield return new WaitForSeconds(duracionAnimacion);
        Destroy(objeto);
        Debug.Log("Game Over");
        FindAnyObjectByType<GameOver>().MostrarGameOver();
    }
}

*/