using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPVehiclePanelScript : MonoBehaviour {


    public Text vehicleName;
    public GameObject vehicleImage;

    int speed, capacity, reliabilty;

    public Transform stat1, stat2, stat3;

    public List<GameObject> stars1, stars2, stars3;
    // Use this for initialization
    void Start()
    {
        stars1 = new List<GameObject>();
        stars2 = new List<GameObject>();
        stars3 = new List<GameObject>();

        for (int i = 0; i < 5; i++)
        {
            stars1.Add(stat1.GetChild(i).gameObject);
            stars2.Add(stat2.GetChild(i).gameObject);
            stars3.Add(stat3.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //speed
        if (speed == 1)
        {
            stars1[0].SetActive(true);
            stars1[1].SetActive(false);
            stars1[2].SetActive(false);
            stars1[3].SetActive(false);
            stars1[4].SetActive(false);
        }
        else if (speed == 2)
        {
            stars1[0].SetActive(true);
            stars1[1].SetActive(true);
            stars1[2].SetActive(false);
            stars1[3].SetActive(false);
            stars1[4].SetActive(false);
        }
        else if (speed == 3)
        {
            stars1[0].SetActive(true);
            stars1[1].SetActive(true);
            stars1[2].SetActive(true);
            stars1[3].SetActive(false);
            stars1[4].SetActive(false);
        }
        else if (speed == 4)
        {
            stars1[0].SetActive(true);
            stars1[1].SetActive(true);
            stars1[2].SetActive(true);
            stars1[3].SetActive(true);
            stars1[4].SetActive(false);
        }
        else if (speed == 5)
        {
            stars1[0].SetActive(true);
            stars1[1].SetActive(true);
            stars1[2].SetActive(true);
            stars1[3].SetActive(true);
            stars1[4].SetActive(true);
        }
        else
        {
            Debug.Log("speed does not have a value between 1 -5" + speed);
        }
        //capacity
        if (capacity == 1)
        {
            stars2[0].SetActive(true);
            stars2[1].SetActive(false);
            stars2[2].SetActive(false);
            stars2[3].SetActive(false);
            stars2[4].SetActive(false);
        }
        else if (capacity == 2)
        {
            stars2[0].SetActive(true);
            stars2[1].SetActive(true);
            stars2[2].SetActive(false);
            stars2[3].SetActive(false);
            stars2[4].SetActive(false);
        }
        else if (capacity == 3)
        {
            stars2[0].SetActive(true);
            stars2[1].SetActive(true);
            stars2[2].SetActive(true);
            stars2[3].SetActive(false);
            stars2[4].SetActive(false);
        }
        else if (capacity == 4)
        {
            stars2[0].SetActive(true);
            stars2[1].SetActive(true);
            stars2[2].SetActive(true);
            stars2[3].SetActive(true);
            stars2[4].SetActive(false);
        }
        else if (capacity == 5)
        {
            stars2[0].SetActive(true);
            stars2[1].SetActive(true);
            stars2[2].SetActive(true);
            stars2[3].SetActive(true);
            stars2[4].SetActive(true);
        }
        else
        {
            Debug.Log("capacity does not have a value between 1 -5");
        }
        //RELIABILITY
        if (reliabilty == 1)
        {
            stars3[0].SetActive(true);
            stars3[1].SetActive(false);
            stars3[2].SetActive(false);
            stars3[3].SetActive(false);
            stars3[4].SetActive(false);
        }
        else if (reliabilty == 2)
        {
            stars3[0].SetActive(true);
            stars3[1].SetActive(true);
            stars3[2].SetActive(false);
            stars3[3].SetActive(false);
            stars3[4].SetActive(false);
        }
        else if (reliabilty == 3)
        {
            stars3[0].SetActive(true);
            stars3[1].SetActive(true);
            stars3[2].SetActive(true);
            stars3[3].SetActive(false);
            stars3[4].SetActive(false);
        }
        else if (reliabilty == 4)
        {
            stars3[0].SetActive(true);
            stars3[1].SetActive(true);
            stars3[2].SetActive(true);
            stars3[3].SetActive(true);
            stars3[4].SetActive(false);
        }
        else if (reliabilty == 5)
        {
            stars3[0].SetActive(true);
            stars3[1].SetActive(true);
            stars3[2].SetActive(true);
            stars3[3].SetActive(true);
            stars3[4].SetActive(true);
        }
        else
        {
            Debug.Log("reliabilty does not have a value between 1 -5");
        }

    }




    public void PassVehicleInfo(int Pspeed, int Pcapacity, int Preliabilty, Texture PvehicleImage, string PvehicleName , Color vehicleColor)
    {
        vehicleName.text = PvehicleName;
        vehicleImage.GetComponent<RawImage>().texture = PvehicleImage;
        speed = Pspeed;
        capacity = Pcapacity;
        reliabilty = Preliabilty;
        vehicleImage.GetComponent<RawImage>().color = vehicleColor;
    }
}
