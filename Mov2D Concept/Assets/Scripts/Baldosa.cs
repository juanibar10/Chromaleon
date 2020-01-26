using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Baldosa : MonoBehaviour
{
    public GameObject prefab;

    private void Awake()
    {
        Instantiate(prefab, transform);
    }

    public void CambiarTipoEnemigo(TipoEnemigo tipo)
    {
        if (transform.childCount != 0)
        {
            if (transform.GetChild(0).GetComponent<NPC>() != null)
                transform.GetChild(0).GetComponent<NPC>().tipoEnemigo = tipo;
        }
    }
}
