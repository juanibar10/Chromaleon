using System.Collections;
using UnityEngine;

[System.Serializable]
public class Grid
{
    [System.Serializable]
    public struct rowData
    {
        public Baldosa[] columna;
    }
    public rowData[] fila = new rowData[11];
}
