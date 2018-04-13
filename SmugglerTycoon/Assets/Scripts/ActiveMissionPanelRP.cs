using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveMissionPanelRP : MonoBehaviour {
    bool Hide = true;
    public Button pullOut;
    public GameObject MissionPanel;

    //Use this to instantiate new buttons to represent each active mission.
    //Create a new script for the button prefab that will call the button's information.
    public List<GameObject> MissionButtons;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Hide)
        {
            MissionPanel.transform.position = new Vector2(-600, 0);
        }
        else
        {
            MissionPanel.transform.position = new Vector2(0, 0);
        }
	}


    public void HideState()
    {
        Hide = !Hide;
    }
}
