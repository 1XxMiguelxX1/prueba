using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaramov : MonoBehaviour
{
    public Transform jugador;   // El jugador (lo asignas en el inspector)
    public Vector2 offset = new Vector2(0f, 0f);  // Por si quieres ajustar la posición

    void Update()
    {
        if (jugador == null)
        {
            Debug.LogWarning("No se asignó el jugador a la cámara.");
            return;
        }

        // Mantener la posición X fija (no se mueve lateralmente)
        Vector3 posicionNueva = transform.position;
        posicionNueva.y = jugador.position.y + offset.y;

        // Aplicar nueva posición a la cámara
        transform.position = posicionNueva;
    }
}
