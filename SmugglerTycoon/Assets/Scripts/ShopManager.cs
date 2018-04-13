using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject agentButton1, agentButton2, agentButton3, agentButton4;
    public GameObject vehicleButton1, vehicleButton2, vehicleButton3, vehicleButton4;

    
    MissionController missionController;

    List<Agent> agentsInShop;
    List<Vehicle> vehiclesInShop;

    private void Awake()
    {
        agentsInShop = new List<Agent>();
        vehiclesInShop = new List<Vehicle>();

        missionController = GameObject.FindGameObjectWithTag("MissionController").GetComponent<MissionController>();
    }

    // Use this for initialization
    void Start()
    {
        agentButton1.GetComponent<ShopButtonScript>().buttonType = ShopButtonScript.ButtonType.Agent;
        agentButton2.GetComponent<ShopButtonScript>().buttonType = ShopButtonScript.ButtonType.Agent;
        agentButton3.GetComponent<ShopButtonScript>().buttonType = ShopButtonScript.ButtonType.Agent;
        agentButton4.GetComponent<ShopButtonScript>().buttonType = ShopButtonScript.ButtonType.Agent;

        vehicleButton1.GetComponent<ShopButtonScript>().buttonType = ShopButtonScript.ButtonType.Vehicle;
        vehicleButton2.GetComponent<ShopButtonScript>().buttonType = ShopButtonScript.ButtonType.Vehicle;
        vehicleButton3.GetComponent<ShopButtonScript>().buttonType = ShopButtonScript.ButtonType.Vehicle;
        vehicleButton4.GetComponent<ShopButtonScript>().buttonType = ShopButtonScript.ButtonType.Vehicle;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AgentBought(float cost, Agent agent)
    {
        missionController.AgentBought(cost, agent);
    }

    public void VehicleBought(float cost, Vehicle vehicle)
    {
        missionController.VehicleBought(cost, vehicle);
    }



    // This method checks with the mission controller to 
    //  see if we can afford this cost
    public bool CanAfford(float cost)
    {
        if(missionController.CanAfford(cost))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadShop()
    {
        agentButton1.GetComponent<Button>().interactable = true;
        agentButton2.GetComponent<Button>().interactable = true;
        agentButton3.GetComponent<Button>().interactable = true;
        agentButton4.GetComponent<Button>().interactable = true;

        vehicleButton1.GetComponent<Button>().interactable = true;
        vehicleButton2.GetComponent<Button>().interactable = true;
        vehicleButton3.GetComponent<Button>().interactable = true;
        vehicleButton4.GetComponent<Button>().interactable = true;

        agentsInShop.Clear();
        vehiclesInShop.Clear();

        for(int i = 0; i < missionController.GetAgentsCount(); i++)
        {
            if(missionController.GetAgentIndex(i).myStatus == Agent.AgentStatus.InShop)
            {
                agentsInShop.Add(missionController.GetAgentIndex(i));
            }
        }

        for (int i = 0; i < missionController.GetVehiclesCount(); i++)
        {
            if (missionController.GetVehicleIndex(i).myStatus == Vehicle.VehicleStatus.InShop)
            {
                vehiclesInShop.Add(missionController.GetVehicleIndex(i));
            }
        }

        ////missionController.GetAgentsInShop(ref agentsInShop);
        //vehiclesInShop = missionController.GetVehiclesInShop();

        // Assign the buttons their respective agents
        if (agentsInShop.Count >= 1)
        {
            agentButton1.GetComponent<ShopButtonScript>().SetAgent(agentsInShop[0]);
            
        }
        else
        {
            agentButton1.GetComponent<Button>().interactable = false;
        }

        if (agentsInShop.Count >= 2)
        {
            agentButton2.GetComponent<ShopButtonScript>().SetAgent(agentsInShop[1]);
        }
        else
        {
            agentButton2.GetComponent<Button>().interactable = false;
        }

        if (agentsInShop.Count >= 3)
        {
            agentButton3.GetComponent<ShopButtonScript>().SetAgent(agentsInShop[2]);
        }
        else
        {
            agentButton3.GetComponent<Button>().interactable = false;
        }

        if (agentsInShop.Count >= 4)
        {
            agentButton4.GetComponent<ShopButtonScript>().SetAgent(agentsInShop[3]);
        }
        else
        {
            agentButton4.GetComponent<Button>().interactable = false;
        }

        // Assign the buttons their respective vehicles
        if (vehiclesInShop.Count >= 1)
        {
            vehicleButton1.GetComponent<ShopButtonScript>().SetVehicle(vehiclesInShop[0]);
        }
        else
        {
            vehicleButton1.GetComponent<Button>().interactable = false;
        }

        if (vehiclesInShop.Count >= 2)
        {
            vehicleButton2.GetComponent<ShopButtonScript>().SetVehicle(vehiclesInShop[1]);
        }
        else
        {
            vehicleButton2.GetComponent<Button>().interactable = false;
        }

        if (vehiclesInShop.Count >= 3)
        {
            vehicleButton3.GetComponent<ShopButtonScript>().SetVehicle(vehiclesInShop[2]);
        }
        else
        {
            vehicleButton3.GetComponent<Button>().interactable = false;
        }

        if (vehiclesInShop.Count >= 4)
        {
            vehicleButton4.GetComponent<ShopButtonScript>().SetVehicle(vehiclesInShop[3]);
        }
        else
        {
            vehicleButton4.GetComponent<Button>().interactable = false;
        }
    }
}
