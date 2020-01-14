using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBaldosas : MonoBehaviour
{
    public Baldosa[] baldosas;
    public Seleccion seleccion;
    public List<Baldosa> baldosasSeleccionadas = new List<Baldosa>();

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
}
