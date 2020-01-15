using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBaldosas : MonoBehaviour
{
    //Contiene todas las baldosas
    public Baldosa[] baldosas;
    //Script de selección
    public Seleccion seleccion;
    //Lista que contiene las baldosas seleccionadas para el combo
    public List<Baldosa> baldosasSeleccionadas = new List<Baldosa>();

    //True si todas estan llenas de algun enemigo, pieza o player
    public bool todasLlenas = true;


    private void Update()
    {
        //seleccion = 0 es el estado de seleccion, si la booleana seleccionada esta en true y la lista no contiene esa casilla se añade a la lista
        if(seleccion.seleccion == 0)
        {
            foreach (var item in baldosas)
            {
                if (item.seleccionada && !baldosasSeleccionadas.Contains(item))
                    baldosasSeleccionadas.Add(item);
            }
        }
        //seleccion = 1, en este estado es el movimiento del player, y se resetean todas las booleanas a seleccionada = false
        if(seleccion.seleccion == 1)
        {
            foreach (var item in baldosas)
            {
                item.seleccionada = false;
            }
        }
    }

    //Función que llama el player para empezar la corrutina de llenar el tablero
    public void ComprobarCasillas()
    {
        StartCoroutine(comprobar(0.0001f));
    }

    //Corrutina que llena el tablero
    public IEnumerator comprobar(float time)
    {
        //Bucle que hasta que no esten todas llenas no sale
        do
        {
            todasLlenas = true;
            foreach (var item in baldosas)
            {
                //Si la casilla esta vacia y la de arriba no es un almacen
                if (item.transform.childCount == 0 && item.upObject != null && item.upObject.transform.childCount > 0 && !item.contienePlayer && !item.upObject.name.Contains("Almacen"))
                {
                    todasLlenas = false;
                    item.upObject.transform.GetChild(0).SetParent(item.transform);
                    item.transform.GetChild(0).transform.localPosition = new Vector3(0, 0.5f, 0);
                    item.color = item.transform.GetComponentInChildren<Hoja>().color;
                    yield return new WaitForSeconds(time);
                }
                //si la casilla esta vacia y la de arriba contiene el player
                else if (item.transform.childCount == 0 && item.upObject != null)
                {
                    //si la de arriba no es un almacen
                    if(item.upObject.upObject != null)
                    {
                        if (item.upObject.contienePlayer && !item.upObject.name.Contains("Almacen"))
                        {
                            todasLlenas = false;
                            if (item.upObject.upObject != null)
                            {
                                if (item.upObject.upObject.transform.childCount > 0)
                                {
                                    item.upObject.upObject.transform.GetChild(0).transform.SetParent(item.upObject.transform);
                                    item.upObject.transform.GetChild(0).transform.localPosition = new Vector3(0, 0.5f, 0);
                                    item.upObject.color = item.upObject.transform.GetComponentInChildren<Hoja>().color;
                                    yield return new WaitForSeconds(time);
                                }
                            }
                        }
                    }
                    //si la de arriba es un almacen
                    else
                    {
                        if (item.transform.childCount == 0 && item.upObject.upObjectAlmacen != null )
                        {
                            if (!item.contienePlayer)
                            {
                                GameObject objeto = Instantiate(item.upObject.upObjectAlmacen.Almacen[0].gameObject);
                                item.upObject.upObjectAlmacen.Almacen.RemoveAt(0);
                                objeto.transform.SetParent(item.transform);
                                objeto.transform.localPosition = new Vector3(0, 0.5f, 0);
                                item.transform.GetChild(0).transform.localRotation = Quaternion.Euler(-90, 0, -90);
                                objeto.transform.localScale = new Vector3(10, 10, 30);
                                item.transform.GetChild(0).transform.localPosition = new Vector3(0, 0.5f, 0);
                                item.transform.GetChild(0).transform.localRotation = Quaternion.Euler(-90, 0, -90);
                                item.color = item.transform.GetComponentInChildren<Hoja>().color;
                                yield return new WaitForSeconds(time);
                            }
                        }
                    }
                }
                //Penultima linea de arriba
                else if (item.transform.childCount == 0 && item.upObject == null  && item.upObjectAlmacen != null)
                {
                    if (!item.contienePlayer)
                    {
                        GameObject objeto = Instantiate(item.upObjectAlmacen.Almacen[0].gameObject);
                        item.upObjectAlmacen.Almacen.RemoveAt(0);
                        objeto.transform.SetParent(item.transform);
                        objeto.transform.localPosition = new Vector3(0, 0.5f, 0);
                        item.transform.GetChild(0).transform.localRotation = Quaternion.Euler(-90, 0, -90);
                        objeto.transform.localScale = new Vector3(10, 10, 30);
                        item.transform.GetChild(0).transform.localPosition = new Vector3(0, 0.5f, 0);
                        item.transform.GetChild(0).transform.localRotation = Quaternion.Euler(-90, 0, -90);
                        item.color = item.transform.GetComponentInChildren<Hoja>().color;
                        yield return new WaitForSeconds(time);
                    }
                }
            }
        }
        while (!todasLlenas);
    }


}
