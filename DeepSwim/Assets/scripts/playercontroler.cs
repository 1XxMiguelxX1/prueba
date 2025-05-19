using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class playercontroler : MonoBehaviour
{
    public float fuerzaVuelo = 40f; // Empuje hacia arriba por segundo
    public float gravedad = -30f;   // Gravedad personalizada
    private Rigidbody2D rb;
    private Animator Animator;
    public bool estaMuerto = false;

    private float velocidadVertical = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        rb.gravityScale = 0f; // Desactivamos la gravedad de Unity
    }

    void Update()
    {
        if (estaMuerto) return; // ¡Esto bloquea la entrada y movimiento!

        // Detectar si el jugador mantiene presionado
        if (Input.GetMouseButton(0))
        {
            velocidadVertical += fuerzaVuelo * Time.deltaTime;
            Animator.SetBool("subida", true);
            Animator.SetBool("bajada", false);
        }
        else
        {
            velocidadVertical += gravedad * Time.deltaTime;

            if (Mathf.Abs(velocidadVertical) < 0.1f)
            {
                Animator.SetBool("subida", false);
                Animator.SetBool("bajada", false);
            }
            else if (velocidadVertical < 0)
            {
                Animator.SetBool("bajada", true);
                Animator.SetBool("subida", false);
            }
        }

        // Aplicar la velocidad al Rigidbody
        rb.velocity = new Vector2(0, velocidadVertical);

        // Opcional: Limitar velocidad vertical para que no sea demasiado rápida
        velocidadVertical = Mathf.Clamp(velocidadVertical, -15f, 15f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("vida"))
        {
            GameManager.instance.GanarVida();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("punto"))
        {
            Debug.Log("Obtuviste punto");
            GameManager.instance.SumarPunto();
           // Destroy(collision.gameObject);
        }

      
    }



}
