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
}
