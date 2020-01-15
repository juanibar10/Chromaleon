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
        do
        {
            todasLlenas = true;
            foreach (var item in baldosas)
            {
                if (item.transform.childCount == 0 && item.upObject != null && item.upObject.transform.childCount != 0)
                {
                    todasLlenas = false;
                    item.upObject.transform.GetChild(0).SetParent(item.transform);
                    item.transform.GetChild(0).transform.localPosition = Vector3.zero;
                    item.color = item.transform.GetComponentInChildren<Hoja>().color;
                }
            }
        }
        while (!todasLlenas);

       
    }

}
