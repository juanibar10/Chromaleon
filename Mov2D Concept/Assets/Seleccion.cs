using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccion : MonoBehaviour
{
    public GameObject objetoSeleccion;
    public Camera nonVRCamera;
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
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = nonVRCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "Centro")
                        {
                            foreach (var item in baldosaActual.adjacentObjects)
                            {
                                if (hit.transform.name == item.name)
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
                    Ray ray = nonVRCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "Centro" && baldosaActual.color == hit.transform.gameObject.GetComponent<Baldosa>().color && baldosaActual.adjacentObjects.Contains(hit.transform.gameObject) && !managerBaldosas.baldosasSeleccionadas.Contains(hit.transform.gameObject.GetComponent<Baldosa>()))
                        {
                            objeto.transform.position = hit.transform.position;
                            baldosaActual = hit.transform.gameObject.GetComponent<Baldosa>();
                        }
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    seleccion = 1;
                    seleccionando = false;
                }
            }
        }

        if (seleccion == 1)
            Destroy(objeto);
        
           
    }
}
