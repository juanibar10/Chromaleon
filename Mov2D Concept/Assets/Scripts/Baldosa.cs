using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Colores
{
    Azul,
    Rojo,
    Verde,
    Ninguno,
}

public class Baldosa : MonoBehaviour
{
    public bool contieneEnemigo;
    public bool contienePlayer;
    public bool seleccionada;
    public Colores color;

    public List<GameObject> adjacentObjects = new List<GameObject>();
    public Baldosa upObject;
    public AlmacenHojas upObjectAlmacen;

    private void Awake()
    {
        getNearbyObjecs(1.2f);
        getDownObject();
        color = transform.GetComponentInChildren<Hoja>().color;
    }

    private void Update()
    {
        if (transform.childCount == 0)
            contieneEnemigo = false;
        else
            contieneEnemigo = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Seleccion")
            seleccionada = true;

        if (collision.tag == "Player")
            contienePlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            contienePlayer = false;
    }


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

}
