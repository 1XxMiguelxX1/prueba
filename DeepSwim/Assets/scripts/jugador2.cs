using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//JUGADOR DOOOOOSSSSSS

public class jugador2 : MonoBehaviour
{
    public float fuerzaVuelo = 3f;
    public float gravedad = 9f;
    private Rigidbody2D rb;
    private Animator Animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        //        rb.gravityScale = 0; // Desactiva la gravedad de Unity


    }

    void Update()
{
    if (Input.GetKey(KeyCode.Space))  // Mientras esté presionada la barra espaciadora
    {
       
        rb.velocity = new Vector2(rb.velocity.x, fuerzaVuelo);
        Animator.SetBool("subida", true);
        Animator.SetBool("bajada", false);
    }
    else
    {
        rb.velocity += new Vector2(0, -gravedad * Time.deltaTime);

        // Si la velocidad en Y es aproximadamente 0, cambiar a idle
        if (Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            Animator.SetBool("subida", false);
            Animator.SetBool("bajada", false);
        }
        // Si la velocidad es negativa, activar la animación de bajada
        else if (rb.velocity.y < 0)
        {
            Animator.SetBool("bajada", true);
            Animator.SetBool("subida", false);
        }
    }
}}

    /*  void Update()
      {
          if (Input.GetMouseButton(0))  // Mientras esté presionado el clic izquierdo
          {
              rb.velocity = new Vector2(rb.velocity.x, fuerzaVuelo);
              Animator.SetBool("subida", true);
          }
          else
          {
              rb.velocity += new Vector2(0, -gravedad * Time.deltaTime);
              Animator.SetBool("bajada", true);

               Animator.SetBool("subida", false); // Se apaga cuando sueltas el clic
          }
      } */

    /* void Update()
     {
         if (Input.GetMouseButton(0))  // Mientras esté presionado el clic izquierdo
         {
             rb.velocity = new Vector2(rb.velocity.x, fuerzaVuelo);
            // Animator.SetBool("subida", fuerzaVuelo != 0.0f);
         }

         else
         {
             rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravedad * Time.deltaTime);
         }
     } */

   // private void OnTriggerEnter2D(Collider2D collision)
   // {
     //   if (collision.CompareTag("punto"))
      //  {
         //   Debug.Log("Obtuviste punto");
         //   GameManager.instance.SumarPunto();
       // }
    //    else if (collision.CompareTag("obstaculo"))
    //    {
    //        Debug.Log("Game Over");
    //        FindAnyObjectByType<GameOver>().MostrarGameOver();
    //    }
    //}




//}