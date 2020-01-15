using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccion : MonoBehaviour
{
    public GameObject objetoSeleccion;
    public Camera camara;
    public LayerMask layer;
    public GameObject objeto;
    public bool seleccionando;
    public int seleccion = 0;

    public Baldosa baldosaActual;

    public ManagerBaldosas managerBaldosas;

    private void Update()
    {
        if(seleccion == 0)
        {
            if (!seleccionando)
            {
                //Manda un rayo desde la posicion del raton al mapa y si colisiona con una casilla crea el seleccionador en el centro de esta si cumple las condiciones
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = camara.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "Centro")
                        {
                            foreach (var item in baldosaActual.adjacentObjects)
                            {
                                //si la casilla es una de las adyacentes y si el color es diferente a ninguno
                                if (hit.transform.name == item.name && hit.transform.gameObject.GetComponent<Baldosa>().color != Colores.Ninguno)
                                {
                                    objeto = Instantiate(objetoSeleccion, hit.transform.position, Quaternion.identity);
                                    baldosaActual = hit.transform.gameObject.GetComponent<Baldosa>();
                                    seleccionando = true;
                                }
                            }
                        }
                    }
                }
            }

            if (seleccionando)
            {
                if (objeto != null)
                {
                    RaycastHit hit;
                    Ray ray = camara.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        // mueve el seleccionador si la casilla es una adyacente y si el color es el mismo que la anterior y si la casilla no esta seleccionada previamente
                        if (hit.transform.tag == "Centro" && baldosaActual.color == hit.transform.gameObject.GetComponent<Baldosa>().color && baldosaActual.adjacentObjects.Contains(hit.transform.gameObject) && !managerBaldosas.baldosasSeleccionadas.Contains(hit.transform.gameObject.GetComponent<Baldosa>()))
                        {
                            objeto.transform.position = hit.transform.position;
                            baldosaActual = hit.transform.gameObject.GetComponent<Baldosa>(); 
                        }
                    }
                }
                // si se levanta el click el estado de seleccion para y se borra el seleccionador
                if (Input.GetMouseButtonUp(0))
                {
                    seleccion = 1;
                    seleccionando = false;
                    managerBaldosas.baldosasSeleccionadas[managerBaldosas.baldosasSeleccionadas.ToArray().Length - 1].color = Colores.Ninguno;
                }
            }
        }

        if (seleccion == 1)
        {
            objeto.transform.GetChild(0).SetParent(null);
            Destroy(objeto);
        }
    }
}
