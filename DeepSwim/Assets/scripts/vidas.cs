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
        InicializarCorazones();
        ActualizarCorazones(GameManager.instance.vidas);
    }

    void InicializarCorazones()
    {
        // Instancia los corazones una sola vez
        for (int i = 0; i < vidasMaximas; i++)
        {
            GameObject nuevoCorazon = Instantiate(corazonPrefab, transform);
            corazones.Add(nuevoCorazon);
        }
    }

    public void ActualizarCorazones(int cantidad)
    {
        for (int i = 0; i < corazones.Count; i++)
        {
            corazones[i].SetActive(i < cantidad);
        }
    }
}
