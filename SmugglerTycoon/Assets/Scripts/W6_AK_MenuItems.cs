#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class W6_AK_MenuItems : MonoBehaviour
{
    static GameObject pathNodePrefab;
    static GameObject pathContainerPrefab;

    /// <summary>
    /// creates a new path as a child of the 'travel location'. only available if only one 'travel location' is selected.
    /// </summary>
    [MenuItem("Path Manager/Create New Path", false)]
    public static void CreateNewPath()
    {
        pathNodePrefab = Resources.Load("PF_pathNode") as GameObject;
        pathContainerPrefab = Resources.Load("PF_PathContainer") as GameObject;

        GameObject pathContainer = Instantiate(pathContainerPrefab, Selection.transforms[0]);
        pathContainer.transform.position = Selection.transforms[0].position;
        pathContainer.SendMessage("BuildPath");

        Selection.transforms[0].GetComponent<MapInteractable>().paths.Add(pathContainer);
    }

    [MenuItem("Path Manager/Create New Path", true)]
    public static bool ValidateCreateNewPath()
    {
        bool valid = false;
        if(Selection.transforms.Length == 1)
        {
            valid = Selection.transforms[0].gameObject.CompareTag("TravelLocation");
        }
        return valid;
    }


    /// <summary>
    /// edits a path in a pathContainer. only available if only one 'pathContainer' is selected.
    /// </summary>
    [MenuItem("Path Manager/Edit Path", false)]
    public static void EditPath()
    {
        GameObject pathContainer = Selection.transforms[0].gameObject;
        pathContainer.SendMessage("BuildPath");
    }

    [MenuItem("Path Manager/Edit Path", true)]
    public static bool ValidateEditPath()
    {
        bool valid = false;
        if (Selection.transforms.Length == 1)
        {
            valid = Selection.transforms[0].gameObject.CompareTag("pathContainer");
        }
        return valid;
    }
}
#endif