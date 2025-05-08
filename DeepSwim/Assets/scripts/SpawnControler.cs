using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControler : MonoBehaviour
{
    public GameObject pulpoprueba;
    public float TiempoSpawn = 2f;
    public float minY = -5.8f;
    public float maxY = 13f;

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
        Instantiate (pulpoprueba, spawnPosition, Quaternion.identity);
    }





}

