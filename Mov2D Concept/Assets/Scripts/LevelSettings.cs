using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public static LevelSettings current;

    [Header("Enemigos en el nivel")]
    public int NPCCount;

    [Header("Porcentaje de Moscas")]
    [Range(0,100)]
    public float flyPercentage;

    [Header("Porcentaje de Buhos")]
    [Range(0, 100)]
    public float owlPercentage;

    [Header("Porcentaje de Sapos")]
    [Range(0, 100)]
    public float toadPercentage;

    [Header("Porcentaje de Hojas")]
    [Range(0, 100)]
    public float leafPercentage;

    [Header("Porcentaje de Flores Blancas")]
    [Range(0, 100)]
    public float colorFLowerPercentage;

    [Header("Numero de enemigos por variable")]

    public int flyCount;
    public int owlCount;
    public int toadCount;
    public int leafCount;
    public int colorFlowerCount;

    [Header("Numero de enemigos reales por variable")]
    public int realFlyCount;
    public int realOwlCount;
    public int realToadCount;
    public int realLeafCount;
    public int realColorFlowerCount;


    private void Awake()
    {
        flyCount =          (int)((flyPercentage * NPCCount) / 100);
        owlCount =          (int)((owlPercentage * NPCCount) / 100);
        toadCount =         (int)((toadPercentage * NPCCount) / 100);
        leafCount =         (int)((leafPercentage * NPCCount) / 100);
        colorFlowerCount =  (int)((colorFLowerPercentage * NPCCount) / 100);

        int total = flyCount + owlCount + toadCount + leafCount + colorFlowerCount;
        if (total < NPCCount)
            leafCount += NPCCount - total;
    }

}
