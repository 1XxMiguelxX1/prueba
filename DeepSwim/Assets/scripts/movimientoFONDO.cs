using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoFONDO : MonoBehaviour
{
   public Renderer fondo;
    public Renderer[] detalle;

    // Update is called once per frame
    void Update()
    {
        fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.2f, 0) * Time.deltaTime;
        detalle[0].material.mainTextureOffset = detalle[0].material.mainTextureOffset + new Vector2(0.02f, 0) * Time.deltaTime;
        detalle[1].material.mainTextureOffset = detalle[1].material.mainTextureOffset + new Vector2(0.3f, 0) * Time.deltaTime;
        detalle[2].material.mainTextureOffset = detalle[2].material.mainTextureOffset + new Vector2(0.25f, 0) * Time.deltaTime;
        detalle[3].material.mainTextureOffset = detalle[3].material.mainTextureOffset + new Vector2(0.3f, 0) * Time.deltaTime;
    
    }
}
