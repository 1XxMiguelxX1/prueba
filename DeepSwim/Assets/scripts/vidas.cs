using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vidas : MonoBehaviour
{
    public GameObject corazonPrefab; // Prefab del corazón
    public int vidasMaximas = 3;
    private List<GameObject> corazones = new List<GameObject>();

    void Start()
    {
        ActualizarCorazones(GameManager.instance.vidas);
    }

    public void ActualizarCorazones(int cantidad)
    {
        // Elimina todos los corazones actuales
        foreach (GameObject c in corazones)
        {
            Destroy(c);
        }
        corazones.Clear();

        // Crea corazones nuevos según la cantidad actual
        for (int i = 0; i < cantidad; i++)
        {
            GameObject nuevoCorazon = Instantiate(corazonPrefab, transform);
            corazones.Add(nuevoCorazon);
        }
    }
}
