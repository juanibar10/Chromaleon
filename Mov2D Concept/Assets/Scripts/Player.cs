using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class Player : MonoBehaviour
{
    //Script de seleccion
    public Seleccion seleccion;
    //Script de manager de las baldosas
    public ManagerBaldosas baldosas;

    public bool modoJuego;

    public Text modoJuegoTexto;

    public GameObject contenedorTextoCambioHojas;
    public int cambiosHoja;
    public int cambiosHojaMaximos;
    public Text cambiosHojaTexto;

    //Lista con objetos de las casillas adyacentes
    public List<GameObject> adjacentObjects = new List<GameObject>();
    public float time;
    public float forTime;
    private void Awake()
    {
        getNearbyObjecs(2.6f);
    }

    public void empezarMoverJugador()
    {
        StartCoroutine(MoverJugador(0.0001f));
        seleccion.seleccion = 2;
    }

    public void cambiarModo()
    {
        if (!seleccion.seleccionando && seleccion.seleccion == 0)
            modoJuego = !modoJuego;

        if (cambiosHoja < 0)
            modoJuego = false;
        contenedorTextoCambioHojas.SetActive(modoJuego);
    }

    private void Update()
    {
        if (modoJuego)
            modoJuegoTexto.text = "Modo mover hojas";
        else
            modoJuegoTexto.text = "Modo trazar camino";

        cambiosHojaTexto.text = cambiosHoja.ToString();
    }

    public IEnumerator MoverJugador(float tiempo)
    {
        forTime = 50;
        //Mover el jugador por cada baldosa seleccionada
        for (int i = 0; i < baldosas.baldosasSeleccionadas.ToArray().Length; i++)
        {
            float auxX = transform.position.x;
            float auxZ = transform.position.z;
            //Shake de la camara al matar cada enemigo
            CameraShaker.Instance.ShakeOnce(3, 7, 0.1f, 0.3f);

            //for de transicion entre baldosas
            for (int j = 0; j <= forTime; j++)
            {
                transform.position = new Vector3(Mathf.Lerp(auxX, baldosas.baldosasSeleccionadas[i].transform.position.x, j * 1/forTime), transform.position.y, Mathf.Lerp(auxZ, baldosas.baldosasSeleccionadas[i].transform.position.z, j * 1/forTime));
                forTime = forTime / 1.005f;
                yield return new WaitForSeconds(tiempo * 1/forTime);
            }
            if(baldosas.baldosasSeleccionadas[i].GetComponent<Baldosa>().tipoEnemigo == TiposEnemigo.Mosca)
            {
                cambiosHoja = cambiosHojaMaximos;
            }
            //Se establece el color de la casilla a ninguno y se destruye el enemigo de esa casilla
            baldosas.baldosasSeleccionadas[i].transform.GetComponent<Baldosa>().color = Colores.Ninguno;
            baldosas.baldosasSeleccionadas[i].seleccionada = false;
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
        adjacentObjects = new List<GameObject>();
        getNearbyObjecs(2.6f);
    }

    void getNearbyObjecs(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Centro")
                adjacentObjects.Add(hitColliders[i].gameObject);
            i++;
        }
    }
}
