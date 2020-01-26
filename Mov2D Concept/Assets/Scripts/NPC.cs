using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoEnemigo
{
    Vacio,
    Mosca,
    Buho,
    Sapo,
    Aguila,
    Hoja,
    FlorBlanca,
}

public class NPC : MonoBehaviour
{
    public TipoEnemigo tipoEnemigo;
}
