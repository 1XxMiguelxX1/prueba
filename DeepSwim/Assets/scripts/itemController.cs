using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{
    public float velocidad = 8f;

    void Start()
    {
        Destroy(gameObject, 5.3f);
    }

    void Update()
    {
        transform.position += Vector3.left * velocidad * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga este tag
        {
            GameManager.instance.SumarPunto();
            Destroy(gameObject); // Desaparece el item
        }
    }
}
