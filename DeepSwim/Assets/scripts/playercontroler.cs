using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    public float fuerzaVuelo = 40f;
    public float gravedad = -30f;
    public bool estaMuerto = false;
    public bool esInvencible = false; // El "escudo" funcional

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;

    private float velocidadVertical = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        if (estaMuerto) return;

        if (Input.GetMouseButton(0))
        {
            velocidadVertical += fuerzaVuelo * Time.deltaTime;
            animator.SetBool("subida", true);
            animator.SetBool("bajada", false);
        }
        else
        {
            velocidadVertical += gravedad * Time.deltaTime;
            if (velocidadVertical < 0)
            {
                animator.SetBool("bajada", true);
                animator.SetBool("subida", false);
            }
        }

        rb.velocity = new Vector2(0, velocidadVertical);
        velocidadVertical = Mathf.Clamp(velocidadVertical, -15f, 15f);
    }

    public void Morir()
    {
        if (estaMuerto) return;
        estaMuerto = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        animator.SetTrigger("muerte");
        playerCollider.enabled = false;
        StartCoroutine(MostrarPanelGameOverTrasAnimacion());
    }

    private IEnumerator MostrarPanelGameOverTrasAnimacion()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = false;
        FindObjectOfType<GameOver>().MostrarGameOver();
    }

    public void Revivir()
    {
        estaMuerto = false;
        rb.isKinematic = false;
        velocidadVertical = 0f;
        spriteRenderer.enabled = true;
        playerCollider.enabled = true;

        if (animator != null)
        {
            animator.Play("statique"); // Forzamos el estado visual correcto
        }

        StartCoroutine(RutinaDeInvencibilidad());
    }

    private IEnumerator RutinaDeInvencibilidad()
    {
        esInvencible = true; // 1. Activamos el escudo

        float tiempoInvencible = 2.0f;
        float fin = Time.time + tiempoInvencible;
        while (Time.time < fin)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Efecto visual
            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.enabled = true; // Nos aseguramos de que termine visible
        esInvencible = false; // 2. Desactivamos el escudo
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estaMuerto || esInvencible) return;

        if (collision.CompareTag("vida"))
        {
            GameManager.instance.GanarVida();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("punto"))
        {
            GameManager.instance.SumarPunto();
        }
    }
}