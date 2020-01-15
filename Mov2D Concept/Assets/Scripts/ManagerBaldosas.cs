using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBaldosas : MonoBehaviour
{
    public Baldosa[] baldosas;
    public Seleccion seleccion;
    public List<Baldosa> baldosasSeleccionadas = new List<Baldosa>();

    public bool todasLlenas = true;


    private void Update()
    {
        if(seleccion.seleccion == 0)
        {
            foreach (var item in baldosas)
            {
                if (item.seleccionada && !baldosasSeleccionadas.Contains(item))
                    baldosasSeleccionadas.Add(item);
            }
        }
        if(seleccion.seleccion == 1)
        {
            foreach (var item in baldosas)
            {
                item.seleccionada = false;
            }
        }
    }

    public void ComprobarCasillas()
    {
        StartCoroutine(comprobar());
    }

    public IEnumerator comprobar()
    {
        do
        {
            todasLlenas = true;
            foreach (var item in baldosas)
            {
                if (item.transform.childCount == 0 && item.upObject != null && item.upObject.transform.childCount > 0 && !item.contienePlayer)
                {
                    todasLlenas = false;
                    item.upObject.transform.GetChild(0).SetParent(item.transform);
                    item.transform.GetChild(0).transform.localPosition = Vector3.zero;
                    item.color = item.transform.GetComponentInChildren<Hoja>().color;
                    yield return new WaitForSeconds(0.05f);
                }
                else if (item.transform.childCount == 0 && item.upObject != null)
                {
                    if (item.upObject.contienePlayer)
                    {
                        print(item.upObject);
                        todasLlenas = false;
                        if (item.upObject.upObject != null)
                        {
                            if (item.upObject.upObject.transform.childCount > 0)
                            {
                                item.upObject.upObject.transform.GetChild(0).transform.SetParent(item.upObject.transform);
                                item.upObject.transform.GetChild(0).transform.localPosition = Vector3.zero;
                                item.upObject.color = item.upObject.transform.GetComponentInChildren<Hoja>().color;
                                yield return new WaitForSeconds(0.05f);
                            }
                        }

                        if (item.upObject != null)
                        {
                            if (item.upObject.transform.childCount > 0)
                            {
                                item.upObject.transform.GetChild(0).SetParent(item.transform);
                                item.transform.GetChild(0).transform.localPosition = Vector3.zero;
                                item.color = item.transform.GetComponentInChildren<Hoja>().color;
                                yield return new WaitForSeconds(0.05f);
                            }
                        } 
                    }
                }
            }
        }
        while (!todasLlenas);
    }


}
