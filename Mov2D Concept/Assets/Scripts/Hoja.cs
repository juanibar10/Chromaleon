using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoja : MonoBehaviour
{
    //Almacena el color y los materiales correspondientes
    public Colores color;
    public Material[] material;

    private void Awake()
    {
        //Asigna los materiales dependiendo el color de la pieza segun se crean
        if (color == Colores.Azul)
           GetComponent<MeshRenderer>().material = material[0];
        else if (color == Colores.Rojo)
           GetComponent<MeshRenderer>().material = material[1];
        else if (color == Colores.Verde)
           GetComponent<MeshRenderer>().material = material[2];
    }
}
