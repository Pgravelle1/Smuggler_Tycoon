using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Camera Cam;
    public bool useInteraction;

    // Use this for initialization
    void Start ()
    {
        if (!Cam)
            Cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetMouseButton(2))
        //{
        //    float mouseX = -Input.GetAxis("Mouse X");
        //    float mouseY = -Input.GetAxis("Mouse Y");
        //    float mouseZ = Input.GetAxis("Mouse ScrollWheel") * 10;
        //    Cam.transform.position = new Vector3(Cam.transform.position.x + mouseX, Cam.transform.position.y + mouseY, Cam.transform.position.z);
        //    Cam.transform.Translate(new Vector3(0, 0, mouseZ));
        //}
        //else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        //{
        //    float mouseZ = Input.GetAxis("Mouse ScrollWheel") * 10;
        //    Cam.transform.Translate(new Vector3(0, 0, mouseZ));
        //}


        //added on
        if (useInteraction)
            checkInteractables();
    }

    /// <summary>
    /// raycasts from camera to mouse position. looks for an object tagged "TravelLocation". if found sends the gameobject a message to invoc a method called "interact".
    /// </summary>
    private void checkInteractables()
    {
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100) && hit.collider.gameObject.CompareTag("TravelLocation"))
        {
            hit.collider.gameObject.SendMessage("interact");
        }
    }
}
