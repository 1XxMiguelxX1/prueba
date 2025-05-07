using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaramov : MonoBehaviour
{
    public Transform jugador;   // El jugador (lo asignas en el inspector)
    public Vector2 offset = new Vector2(0f, 0f);  // Por si quieres ajustar la posici�n

    void Update()
    {
        if (jugador == null)
        {
            Debug.LogWarning("No se asign� el jugador a la c�mara.");
            return;
        }

        // Mantener la posici�n X fija (no se mueve lateralmente)
        Vector3 posicionNueva = transform.position;
        posicionNueva.y = jugador.position.y + offset.y;

        // Aplicar nueva posici�n a la c�mara
        transform.position = posicionNueva;
    }
}
