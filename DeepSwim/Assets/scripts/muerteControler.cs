using System.Collections;
using UnityEngine;

public class MuerteController : MonoBehaviour
{
    // Duración de la animación de muerte (ajústala según tu animación)
    public float duracionAnimacion = .5f;

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