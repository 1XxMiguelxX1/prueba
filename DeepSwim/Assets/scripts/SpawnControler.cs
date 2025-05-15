using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControler : MonoBehaviour
{
    public GameObject[] ItemPrefab;
    public float TiempoSpawn = 2f;
    public float minY = -4f;
    public float maxY = 15f;

    private float timer = 0f;

        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TiempoSpawn)
        {
            SpawnNivel();
            timer = 0f;
        }
    }

    void SpawnNivel()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);

        // Elegir un prefab aleatorio
        int randomIndex = Random.Range(0, ItemPrefab.Length);
        GameObject selectedPrefab = ItemPrefab[randomIndex];

        GameObject nuevo = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        // Voltearlo para que mire a la izquierda
        nuevo.transform.localScale = new Vector3(-1f * nuevo.transform.localScale.x, nuevo.transform.localScale.y, nuevo.transform.localScale.z);
    }





}

