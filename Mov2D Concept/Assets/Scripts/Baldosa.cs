using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Colores
{
    Azul,
    Rojo,
    Verde,
    Amarillo,
}

public class Baldosa : MonoBehaviour
{
    public GameObject enemigo;
    public bool contieneEnemigo;
    public bool contienePlayer;
    public bool seleccionada;
    public Colores color;

    public List<GameObject> adjacentObjects = new List<GameObject>();

    private void Awake()
    {
        enemigo = transform.GetChild(0).gameObject;
        getNearbyObjecs(1.2f);
    }
    

    private void Update()
    {
        if (!contieneEnemigo && enemigo != null)
        {
            enemigo = null;
            Destroy(transform.GetChild(0).gameObject);
        }

        if (contienePlayer)
            contieneEnemigo = false;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Seleccion")
        {
            seleccionada = true;
            print("a");
        }
    }

    void getNearbyObjecs(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].gameObject.name != name && hitColliders[i].tag == "Centro")
                adjacentObjects.Add(hitColliders[i].gameObject);
            i++;
        }
    }
}
