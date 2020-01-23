using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enumerado de colores de piezas, NINGUNO es cuando esta vacia
public enum Colores
{
    Azul,
    Rojo,
    Verde,
    Multicolor,
    Ninguno,
}

public class Baldosa : MonoBehaviour
{
    // Si contiene player o enemigo
    public bool contieneEnemigo;
    public bool contienePlayer;
    //Si esta seleccionada para combo
    public bool seleccionada;
    //El color de la casilla
    public Colores color; 
    //El color de la casilla
    public TiposEnemigo tipoEnemigo;
    //Lista con objetos de las casillas adyacentes
    public List<GameObject> adjacentObjects = new List<GameObject>();
    //Casilla superior
    public Baldosa upObject;
    //Si esta en la linea de arriba se asigna el almacén
    public AlmacenHojas upObjectAlmacen;

    private void Awake()
    {
        //Coge las casillas adyacentes
        getNearbyObjecs(1.2f);
        //Coge la casilla de arriba
        getDownObject();
        //Si contiene algun enemigo u hoja se le asigna el color de este
        if(transform.childCount > 0)
        {
            color = transform.GetComponentInChildren<NPC>().color;
            tipoEnemigo = transform.GetComponentInChildren<NPC>().tipoEnemigo;
        }
    }

    private void Update()
    {
        // Si la casilla esta vacia la variable se pone en false y viceversa
        if (transform.childCount == 0)
            contieneEnemigo = false;
        else
            contieneEnemigo = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Si entra el objeto de seleccion la booleana de seleccion se pone true, mas tarde al terminar el movimiento se resetea
        if (collision.tag == "Seleccion")
            seleccionada = true;

        //Si entra el player en la casilla la booleana de continePlayer se pone true
        if (collision.tag == "Player")
            contienePlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //Si sale el player en la casilla la booleana de continePlayer se pone false
        if (other.tag == "Player")
            contienePlayer = false;
    }

    //Coge los objetos de al rededor generando un collider, y añade a la lista solo los que contienen el tag de Centro
    void getNearbyObjecs(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.name != name && hitColliders[i].tag == "Centro")
                adjacentObjects.Add(hitColliders[i].gameObject);
            i++;
        }
    }
    //Coge eñ objetos de arriba generando un collider, y añade a la lista solo los que contienen el tag de Centro o Almacen, 
    //depende el tag lo asigna a una variable u otra
    void getDownObject()
    {
        Collider[] hitCollider = Physics.OverlapBox(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), new Vector3(0.5f, 0.5f, 0.5f));
        int i = 0;
        while (i < hitCollider.Length)
        {
            if (hitCollider[i].gameObject.name != name && hitCollider[i].tag == "Centro")
                upObject = hitCollider[i].gameObject.GetComponent<Baldosa>();
            else if(hitCollider[i].gameObject.name != name &&  hitCollider[i].tag == "Almacen")
                upObjectAlmacen = hitCollider[i].gameObject.GetComponent<AlmacenHojas>();

            i++;
        }
    }

    public void Actualizar()
    {
        color = transform.GetChild(0).GetComponent<NPC>().color;
        tipoEnemigo = transform.GetChild(0).GetComponent<NPC>().tipoEnemigo;
    }

}
