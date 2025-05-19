using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{
    public float velocidad = 13.3f;
    public float caida = 0.5f; 
    public bool cae = false;

    void Start()
    {
        if (CompareTag("objeto"))
        {
            cae = true;
        }

        Destroy(gameObject, 3.1f);
    }

    void Update()
    {
        Vector3 movimiento = Vector3.left * velocidad;

        if (cae)
        {
            movimiento += Vector3.down * caida; 
        }

        transform.position += movimiento * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga este tag
        {
            //var player = other.GetComponent<playercontroler>();
            //if (player == null || player.estaMuerto) return;
            Destroy(gameObject); // Desaparece el item
        }
    }
}
