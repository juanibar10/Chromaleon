using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Player : MonoBehaviour
{
    public Seleccion seleccion;
    public ManagerBaldosas baldosas;

    private void Update()
    {
        if (seleccion.seleccion == 1)
        {
            StartCoroutine(MoverJugador(0.001f));
            seleccion.seleccion = 2;
        }
    }

    public IEnumerator MoverJugador(float tiempo)
    {
        for (int i = 0; i < baldosas.baldosasSeleccionadas.ToArray().Length; i++)
        {
            float auxX = transform.position.x;
            float auxZ = transform.position.z;
            CameraShaker.Instance.ShakeOnce(3, 7, 0.1f, 0.3f);
            for (int j = 0; j <= 50; j++)
            {
                transform.position = new Vector3(Mathf.Lerp(auxX, baldosas.baldosasSeleccionadas[i].transform.position.x, j * 0.02f), transform.position.y, Mathf.Lerp(auxZ, baldosas.baldosasSeleccionadas[i].transform.position.z, j * 0.02f));
                yield return new WaitForSeconds(tiempo * 0.02f);
            }
            baldosas.baldosasSeleccionadas[i].transform.GetComponent<Baldosa>().color = Colores.Ninguno;
            Destroy(baldosas.baldosasSeleccionadas[i].transform.GetChild(0).gameObject);
            yield return new WaitForSeconds(0.01f);
        }
        seleccion.seleccion = 0;
        baldosas.baldosasSeleccionadas = new List<Baldosa>();
        baldosas.ComprobarCasillas();

    }
}
