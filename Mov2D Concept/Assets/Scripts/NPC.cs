using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiposEnemigo
{
    Hoja,
    Enemigo,
    Mosca,
    HojaMulticolor,
    Ninguno,
}

public class NPC : MonoBehaviour
{
    
    //Almacena el color y los materiales correspondientes
    public Colores color;
    public Material[] material;
    //Almacena el tipo de enmigo
    public TiposEnemigo tipoEnemigo;
    [Header("Solo cambiar si es un enemigo")]
    public float minCombo;

    private void Awake()
    {
        //Asigna los materiales dependiendo el color de la pieza segun se crean
        if (tipoEnemigo != TiposEnemigo.HojaMulticolor) 
        { 
            if (color == Colores.Azul)
                GetComponent<MeshRenderer>().material = material[0];
            else if (color == Colores.Rojo)
                GetComponent<MeshRenderer>().material = material[1];
            else if (color == Colores.Verde)
                GetComponent<MeshRenderer>().material = material[2];
        }
        else
        {
            GetComponent<MeshRenderer>().material = material[3];
            color = Colores.Multicolor;
        }

    }
}
