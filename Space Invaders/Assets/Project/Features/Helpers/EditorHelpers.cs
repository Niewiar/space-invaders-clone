#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorHelpers
{
    [MenuItem("GameObject/Sort Children by Name", false, 0)]
    private static void SortChildrenByName()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject obj in selectedObjects)
        {
            SortChildrenByName(obj.transform);
        }
    }

    private static void SortChildrenByName(Transform parent)
    {
        int childCount = parent.childCount;
        List<Transform> children = new List<Transform>();

        for (int i = 0; i < childCount; i++)
        {
            children.Add(parent.GetChild(i));
        }

        children.Sort((a, b) => EditorUtility.NaturalCompare(a.name, b.name));

        for (int i = 0; i < childCount; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }
}
#endif
