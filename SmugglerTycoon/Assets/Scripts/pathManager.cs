using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class pathManager : MonoBehaviour
{
    bool CanSetPathNodes = false;
    public List<GameObject> path = new List<GameObject>();
    GameObject pathNodePrefab;

    int lastMouseButton = -1;
    int currentMouseButton = -1;

    LineRenderer lr;
    private void Start()
    {
        DrawPath();
    }

    private void DrawPath()
    {
        lr = GetComponent<LineRenderer>();
        pathNode[] nodes = GetComponentsInChildren<pathNode>();
        path.Clear();
        for (int i = 0; i < nodes.Length; i++)
        {
            path.Add(nodes[i].gameObject);
        }
        lr.positionCount = path.Count + 1;
        lr.SetPosition(0, transform.position);
        for (int i = 0; i < path.Count; i++)
        {
            lr.SetPosition(i + 1, path[i].transform.position);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        lastMouseButton = currentMouseButton;
        currentMouseButton = Event.current.button;
        if (CanSetPathNodes)
        {
            RaycastHit hit;
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Gizmos.color = new Color(1f, 0, 1f, 0.5f);
                Gizmos.DrawSphere(hit.point + new Vector3(0, 0, 3f), 0.5f);
                if (currentMouseButton == 1 && lastMouseButton != 1)
                {
                    GameObject node = Instantiate(pathNodePrefab, transform);
                    node.transform.position = new Vector3(hit.point.x, hit.point.y, transform.position.z);
                    path.Add(node);
                }
            }

            if(currentMouseButton == 2 && lastMouseButton != 2)
            {
                CanSetPathNodes = false;
            }
        }
    }
#endif
    void BuildPath()
    {
        CanSetPathNodes = true;
        pathNodePrefab = Resources.Load("PF_pathNode") as GameObject;
    }

    private void Update()
    {


    }
}
