using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractable : MonoBehaviour
{

    bool interacting = false;
    //the image to display when interacting with location
    public GameObject locationImage;
    public List<GameObject> paths;
    public int LocationNumber;
    // When creating a travel point, make sure to drag and drop
    //  a location image into the picture game object
    //  that is inside of the canvas of the travel point

	void Start ()
    {

    }
	

	void Update ()
    {
        if (!OptionController.Ispaused)
        {
            if (interacting)
            {
                locationImage.SetActive(true);

                for (int i = 0; i < paths.Count; i++)
                {
                    if (paths[i])
                    {
                        paths[i].SetActive(true);
                    }
                    else
                    {
                        paths.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                locationImage.SetActive(false);
                for (int i = 0; i < paths.Count; i++)
                {
                    if (paths[i])
                    {
                        paths[i].SetActive(false);
                    }
                    else
                    {
                        paths.RemoveAt(i);
                        i--;
                    }

                }
            }
        }
        interacting = false;
	}

    internal void interact()
    {
        interacting = true;
    }

    internal void AddPath(GameObject path)
    {
        paths.Add(path);
    }

    private void OnMouseDown()
    {
        MissionController missionGod = GameObject.FindGameObjectWithTag("MissionController").GetComponent<MissionController>();
        //missionGod.StartMission();
        List<Vector3> positions = new List<Vector3>();
        LineRenderer lr = GetComponentInChildren<LineRenderer>();
        if (lr)
        {
            for (int i = 0; i < lr.positionCount; i++)
            {
                positions.Add(lr.GetPosition(i));
            }

            missionGod.OpenMissionSetup(LocationNumber, positions);
        }

    }
}
