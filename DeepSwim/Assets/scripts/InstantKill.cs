using UnityEngine;

public class InstantKill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject collidedObject)
    {
        if (collidedObject.CompareTag("Player"))
        {
            playercontroler pc = collidedObject.GetComponent<playercontroler>();

            // LA LÍNEA MÁS IMPORTANTE:
            // Si el jugador no existe, ya está muerto, O ES INVENCIBLE, no hagas nada.
            if (pc == null || pc.estaMuerto || pc.esInvencible)
            {
                return;
            }

            // Si pasa el chequeo, entonces sí, mátalo.
            GameManager.instance.Cmuere();
        }
    }
}