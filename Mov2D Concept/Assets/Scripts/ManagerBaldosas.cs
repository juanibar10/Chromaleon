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
        StartCoroutine(comprobar(0.0001f));
    }

    public IEnumerator comprobar(float time)
    {
        do
        {
            todasLlenas = true;
            foreach (var item in baldosas)
            {
                if (item.transform.childCount == 0 && item.upObject != null && item.upObject.transform.childCount > 0 && !item.contienePlayer && !item.upObject.name.Contains("Almacen"))
                {
                    todasLlenas = false;
                    item.upObject.transform.GetChild(0).SetParent(item.transform);
                    item.transform.GetChild(0).transform.localPosition = new Vector3(0, 0.5f, 0);
                    item.color = item.transform.GetComponentInChildren<Hoja>().color;
                    yield return new WaitForSeconds(time);
                }
                else if (item.transform.childCount == 0 && item.upObject != null)
                {
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

                            if (item.upObject != null)
                            {
                                if (item.upObject.transform.childCount > 0)
                                {
                                    item.upObject.transform.GetChild(0).SetParent(item.transform);
                                    item.transform.GetChild(0).transform.localPosition = new Vector3(0, 0.5f, 0);
                                    item.color = item.transform.GetComponentInChildren<Hoja>().color;
                                    yield return new WaitForSeconds(time);
                                }
                            }
                        }
                    }
                    else
                    {
                        if(item.transform.childCount == 0 && item.upObject.upObjectAlmacen != null)
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
