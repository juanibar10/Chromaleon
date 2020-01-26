using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For correcto para recorrer array de primero a ultimo
/*
    for (int i = baldosas.fila.Length - 1; i >= 0; i--)
    {
        for (int j = 0; j < baldosas.fila[0].columna.Length; j++)
        {
            baldosas.fila[i].columna[j]...   
        }
    }
*/

public class ManagerBaldosas : MonoBehaviour
{
    public Grid baldosas;
    public LevelSettings levelSettings;
    private float npcCount;
    private int num;

    private int[] probabilities =
    {
        3,3,3,3,3,3,3,3,3,3,
        3,3,3,3,3,3,3,3,3,3,
        3,3,3,3,3,3,3,3,3,3,
        3,3,3,3,3,3,3,3,3,3,
        3,3,3,3,3,3,3,3,3,3,
        3,3,3,3,3,3,3,3,3,3,
        3,3,3,3,3,3,3,3,3,3,
        2,2,2,2,2,2,0,0,0,0,
        0,2,2,2,2,2,1,1,1,1,
        1,1,1,1,1,1,4,4,4,4,
    };
    private void Start()
    {
        GameEvents.current.onMapInstance += MapInstance;
        GameEvents.current.MapInstanceEvent();
    }

    public void MapInstance()
    {
        do
        {
            for (int i = baldosas.fila.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < baldosas.fila[0].columna.Length; j++)
                {
                    do
                    {
                        num = probabilities[Random.Range(0, probabilities.Length)];
                        switch (num)
                        {
                            case 0:
                                if (levelSettings.realFlyCount < levelSettings.flyCount)
                                {
                                    levelSettings.realFlyCount++;
                                    baldosas.fila[i].columna[j].CambiarTipoEnemigo(TipoEnemigo.Mosca);
                                }
                                else
                                {
                                    num = 5;
                                }
                                break;
                            case 1:
                                if (levelSettings.realOwlCount < levelSettings.owlCount)
                                {
                                    levelSettings.realOwlCount++;
                                    baldosas.fila[i].columna[j].CambiarTipoEnemigo(TipoEnemigo.Buho);
                                }
                                else
                                {
                                    num = 5;
                                }
                                break;
                            case 2:
                                if (levelSettings.realToadCount < levelSettings.toadCount)
                                {
                                    levelSettings.realToadCount++;
                                    baldosas.fila[i].columna[j].CambiarTipoEnemigo(TipoEnemigo.Sapo);
                                }
                                else
                                {
                                    num = 5;
                                }
                                break;
                            case 3:
                                if (levelSettings.realLeafCount < levelSettings.leafCount)
                                {
                                    levelSettings.realLeafCount++;
                                    baldosas.fila[i].columna[j].CambiarTipoEnemigo(TipoEnemigo.Hoja);
                                }
                                else
                                {
                                    num = 5;
                                }
                                break;
                            case 4:
                                if (levelSettings.realColorFlowerCount < levelSettings.colorFlowerCount)
                                {
                                    levelSettings.realColorFlowerCount++;
                                    baldosas.fila[i].columna[j].CambiarTipoEnemigo(TipoEnemigo.FlorBlanca);
                                }
                                else
                                {
                                    num = 5;
                                }
                                break;
                        }
                    } while (num < 0 || num >= 5);
                    npcCount++;
                }
            }
        } while (npcCount < 77);
        print("Ha acabado");
    }
}
