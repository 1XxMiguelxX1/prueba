using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class metraje : MonoBehaviour

{
    public TMP_Text metrosText;
    public float velocidadMetrosPorSegundo = 5f;

    private float distancia = 0f;
    private float tiempo = 0f;
    private bool estaJugando = true;

    void Update()
    {
        if (!estaJugando) return;

        tiempo += Time.deltaTime;
        distancia = tiempo * velocidadMetrosPorSegundo;

        metrosText.text = "Metros: " + Mathf.FloorToInt(distancia).ToString();
    }

    public void Reiniciar()
    {
        tiempo = 0f;
        distancia = 0f;
        estaJugando = true;
    }

    public void Detener()
    {
        estaJugando = false;
    }

    public int GetDistancia()
    {
        return Mathf.FloorToInt(distancia);
    }
}