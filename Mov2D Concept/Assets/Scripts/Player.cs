using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Player : MonoBehaviour
{
    //Script de seleccion
    public Seleccion seleccion;
    //Script de manager de las baldosas
    public ManagerBaldosas baldosas;

    private void Update()
    {
        //Si el estado es el de mover al jugador se activa la corrutina y se cambia el estado seguidamente para que no entre mas de una vez en la corrutina
        if (seleccion.seleccion == 1)
        {
            StartCoroutine(MoverJugador(0.001f));
            seleccion.seleccion = 2;
        }
    }

    public IEnumerator MoverJugador(float tiempo)
    {
        //Mover el jugador por cada baldosa seleccionada
        for (int i = 0; i < baldosas.baldosasSeleccionadas.ToArray().Length; i++)
        {
            float auxX = transform.position.x;
            float auxZ = transform.position.z;
            //Shake de la camara al matar cada enemigo
            CameraShaker.Instance.ShakeOnce(3, 7, 0.1f, 0.3f);
            //for de transicion entre baldosas
            for (int j = 0; j <= 50; j++)
            {
                transform.position = new Vector3(Mathf.Lerp(auxX, baldosas.baldosasSeleccionadas[i].transform.position.x, j * 0.02f), transform.position.y, Mathf.Lerp(auxZ, baldosas.baldosasSeleccionadas[i].transform.position.z, j * 0.02f));
                yield return new WaitForSeconds(tiempo * 0.02f);
            }
            //Se establece el color de la casilla a ninguno y se destruye el enemigo de esa casilla
            baldosas.baldosasSeleccionadas[i].transform.GetComponent<Baldosa>().color = Colores.Ninguno;
            Destroy(baldosas.baldosasSeleccionadas[i].transform.GetChild(0).gameObject);
            //Se espera un tiempo para que el cambio de casilla tenga descansos pequeños entre si
            yield return new WaitForSeconds(0.01f);
        }
        //al terminar se establece el estado al de poder seleccionar y se vacia la lista de casillas seleccionadas
        seleccion.seleccion = 0;
        baldosas.baldosasSeleccionadas = new List<Baldosa>();
        //se comprueba el tablero y si falta alguna ficha se llena
        Destroy(GameObject.FindGameObjectWithTag("Trail").gameObject);
        baldosas.ComprobarCasillas();

    }
}
