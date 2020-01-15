using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoja : MonoBehaviour
{
    public Colores color;
    public Material[] material;

    private void Awake()
    {
        if (color == Colores.Azul)
           GetComponent<MeshRenderer>().material = material[0];
        else if (color == Colores.Rojo)
           GetComponent<MeshRenderer>().material = material[1];
        else if (color == Colores.Verde)
           GetComponent<MeshRenderer>().material = material[2];
    }
}
