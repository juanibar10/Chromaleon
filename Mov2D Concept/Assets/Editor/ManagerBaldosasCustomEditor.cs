using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Grid))]
public class ManagerBaldosasCustomEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        Rect newPosition = position;
        newPosition.y += 18f;
        SerializedProperty data = property.FindPropertyRelative("fila");
        //data baldosas [0][]
        for(int j = 0; j < 11; j++)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("columna");
            newPosition.height = 18f;
            if (row.arraySize != 7)
                row.arraySize = 7;
            newPosition.width = position.width / 7;
            for(int i = 0; i < 7; i++)
            {
                EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(i), GUIContent.none);
                newPosition.x += newPosition.width;
            }
            newPosition.x = position.x;
            newPosition.y += 18f;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * 12;
    }
}
