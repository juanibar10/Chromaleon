using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Seleccion seleccion;
    public ManagerBaldosas baldosas;

    private void Update()
    {
        if (seleccion.seleccion == 1)
        {
            StartCoroutine(MoverJugador());
            seleccion.seleccion = 2;
        }
    }

    public IEnumerator MoverJugador()
    {
        for (int i = 0; i < baldosas.baldosasSeleccionadas.ToArray().Length; i++)
        {
            transform.position = baldosas.baldosasSeleccionadas[i].transform.position;
            baldosas.baldosasSeleccionadas[i].transform.GetComponent<Baldosa>().color = Colores.Ninguno;
            Destroy(baldosas.baldosasSeleccionadas[i].transform.GetChild(0).gameObject);
            yield return new WaitForSeconds(0.5f);
        }
        seleccion.seleccion = 0;
        baldosas.baldosasSeleccionadas = new List<Baldosa>();
        baldosas.ComprobarCasillas();

    }
}
