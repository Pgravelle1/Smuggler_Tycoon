using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#warning Pierre Change
// ENSURE YOU READ THE "Pierre Changes in Editor" DOCUMENT 

public class MissionSetupController : MonoBehaviour
{
    [SerializeField]
    Transform AgentContent;

    [SerializeField]
    Transform VehicleContent;

    [SerializeField]
    GameObject agentScrollView, vehicleScrollView;

    public GameObject agentContent, vehicleContent, selectedAgentImage, selectedVehicleImage;
    Agent selectedAgent;
    public Text subtotal, total, profit;

    public Text riskText;

    public Slider weightSlider;
    public Text sliderText;

    public Button selectedAgentButton, selectedVehicleButton, startMissionButton;

    List<Vector3> path;

    public Text BoardersText;//the text gamobject containing the border number
    public Text LocationNameText;// the text gameobject the holds the location name
    public RawImage LocationImage; // The raw image to display our location image
    public RawImage selectedAgentStat1, selectedAgentStat2, selectedAgentStat3;
    public RawImage selectedVehicleStat1, selectedVehicleStat2, selectedVehicleStat3;
    int agentIndex;//the index in the array that represents the agent
    int vehicleIndex;//the index in the array that represents the vehicle
    List<Agent> loadedAgents;
    List<Vehicle> loadedVehicles;
    Location theLocation;
    public MissionController MController;//a reference to the mission controller
    public List<Texture> starImages = new List<Texture>();


    //thes list store the agent and vehicle indexes. button with value 0 will acces the array position 0. every button that is active will have a corisponding index possition here to refference the agent/vehicle's index number.
    List<int> m_agents_buttonReferences = new List<int>();
    List<int> m_vehicles_buttonRefferences = new List<int>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        subtotal.text = "Subtotal: $" + ((loadedVehicles[vehicleIndex].FuelEconomy * (float)theLocation.bordersToCross) + (weightSlider.value * 100)).ToString();
        sliderText.text = "Product: " + weightSlider.value.ToString() + " Kg";
    }

    public void LoadData(List<Agent> agents, List<Vehicle> vehicles, Location location, List<Vector3> paths)
    {
        loadedAgents = agents;
        loadedVehicles = vehicles;

#warning alex change - nov 25.
        m_agents_buttonReferences.Clear();
        m_vehicles_buttonRefferences.Clear();
        int indexOfItem = 0;


        bool foundAgent = false;
        bool foundVehicle = false;
        LocationNameText.text = location.LocationName;
        LocationImage.texture = location.LocationImage;
        BoardersText.text = "Border Crossings: " + location.bordersToCross;

#warning Pierre Change
        if (location.bordersToCross >= 3)
        {
            riskText.text = "Risk: HIGH";
        }
        else if (location.bordersToCross == 2)
        {
            riskText.text = "Risk: MEDIUM";
        }
        else if (location.bordersToCross == 1)
        {
            riskText.text = "Risk: LOW";
        }
        else
        {
            riskText.text = "RISK VALUE ERROR";
        }

#warning Pierre Change
        // Made it so that only agents we own that are on standby state are shown

#warning alex overiding pierre change: buttons can't be skipped so agent/vehicles not in stadby should be ignored but not the button index itself.
        //ChangeAgent - nov 25.
        //int agentContentImageIndex = 0;
        for (int i = 0; i < AgentContent.childCount; i++)
        {
            #region ovverriden code
            //if(i < agents.Count && agents[i].myStatus == Agent.AgentStatus.Standby)
            //{
            //    AgentContent.GetChild(agentContentImageIndex).gameObject.SetActive(true);
            //    AgentContent.GetChild(agentContentImageIndex).GetComponent<RawImage>().texture = agents[i].agentImage;
            //    agentContentImageIndex++;
            //}
            //else
            //{
            //    AgentContent.GetChild(agentContentImageIndex).gameObject.SetActive(false);
            //}
            #endregion
#warning the new code
            //finds the next agent who should be in the list
            while (indexOfItem < agents.Count && agents[indexOfItem].myStatus != Agent.AgentStatus.Standby)
            {
                indexOfItem++;
            }
            //if there is an agent to add add it
            if (indexOfItem < agents.Count)
            {
                AgentContent.GetChild(i).gameObject.SetActive(true);
                AgentContent.GetChild(i).GetComponent<RawImage>().texture = agents[indexOfItem].agentImage;
                m_agents_buttonReferences.Add(indexOfItem);
                LoadAgentImageStats(indexOfItem);
            }
            //otherwise deactivate the button
            else
            {
                AgentContent.GetChild(i).gameObject.SetActive(false);
            }
            indexOfItem++;

        }


        // Made it so that only vehicles we own that are on standby state are shown
        #region overriden code
        //int vehicleContentImageIndex = 0;
        //for (int i = 0; i < VehicleContent.childCount; i++)
        //{
        //    if (i < vehicles.Count && vehicles[i].myStatus == Vehicle.VehicleStatus.Standby)
        //    {
        //        VehicleContent.GetChild(vehicleContentImageIndex).gameObject.SetActive(true);
        //        VehicleContent.GetChild(vehicleContentImageIndex).GetComponent<RawImage>().texture = vehicles[i].vehicleImage;
        //        vehicleContentImageIndex++;
        //    }
        //    else
        //    {
        //        VehicleContent.GetChild(vehicleContentImageIndex).gameObject.SetActive(false);
        //    }
        //}
        #endregion
        indexOfItem = 0;
        for (int i = 0; i < VehicleContent.childCount; i++)
        {
            while (indexOfItem < vehicles.Count && vehicles[indexOfItem].myStatus != Vehicle.VehicleStatus.Standby)
            {
                indexOfItem++;
            }
            if (indexOfItem < vehicles.Count)
            {
                VehicleContent.GetChild(i).gameObject.SetActive(true);
                VehicleContent.GetChild(i).GetComponent<RawImage>().texture = vehicles[indexOfItem].vehicleImage;
                VehicleContent.GetChild(i).GetComponent<RawImage>().color = vehicles[indexOfItem].VehicleColor;
                m_vehicles_buttonRefferences.Add(indexOfItem);
                LoadVehicleImageStats(indexOfItem);
            }
            else
            {
                VehicleContent.GetChild(i).gameObject.SetActive(false);
            }
            indexOfItem++;
        }

        // Find first agent on standby
        foreach (Agent agent in agents)
        {
            if (agent.myStatus == Agent.AgentStatus.Standby && !foundAgent)
            {
                selectedAgentButton.interactable = true;
                // Display it as the default
                agentIndex = agents.IndexOf(agent);
                selectedAgentImage.GetComponent<RawImage>().texture = agents[agents.IndexOf(agent)].agentImage;



                foundAgent = true;
            }
        }

        if (!foundAgent)
        {
            startMissionButton.interactable = false;
            selectedAgentButton.interactable = false;
        }

        // Find first vehicle on standby
        foreach (Vehicle vehicle in vehicles)
        {
            if (vehicle.myStatus == Vehicle.VehicleStatus.Standby && !foundVehicle)
            {
                selectedVehicleButton.interactable = true;
                // Display it as the default
                vehicleIndex = vehicles.IndexOf(vehicle);
                selectedVehicleImage.GetComponent<RawImage>().texture = vehicles[vehicles.IndexOf(vehicle)].vehicleImage;
#warning alex change - nov 25. adds color to the image
                selectedVehicleImage.GetComponent<RawImage>().color = vehicles[vehicles.IndexOf(vehicle)].VehicleColor;

#warning Pierre Change
                weightSlider.maxValue = vehicles[vehicles.IndexOf(vehicle)].CapacityMaximums;

                foundVehicle = true;
            }
        }

        if (!foundVehicle)
        {
            startMissionButton.interactable = false;
            selectedVehicleButton.interactable = false;
        }


        if (foundVehicle && foundAgent)
        {
            startMissionButton.interactable = true;
        }

        theLocation = location;
        path = paths;
    }
#warning alex changed method slightly - nov 25
    public void ChangeAgent(int buttonNum)
    {
        agentIndex = m_agents_buttonReferences[buttonNum];
        selectedAgentImage.GetComponent<RawImage>().texture = loadedAgents[agentIndex].agentImage;

        LoadAgentImageStats(agentIndex);
    }
#warning alex changed method slightly - nov 25
    public void ChangeVehicle(int buttonNum)
    {
        vehicleIndex = m_vehicles_buttonRefferences[buttonNum];
        selectedVehicleImage.GetComponent<RawImage>().texture = loadedVehicles[vehicleIndex].vehicleImage;
#warning Pierre Change
        weightSlider.maxValue = loadedVehicles[vehicleIndex].CapacityMaximums;
        selectedVehicleImage.GetComponent<RawImage>().color = loadedVehicles[vehicleIndex].VehicleColor;

        LoadVehicleImageStats(vehicleIndex);
    }
    public void CallStartMission()
    {
        MController.StartMission(agentIndex, vehicleIndex, theLocation, path);
    }

#warning Pierre Change
    // Added these two methods to make it so when we click on
    //  the main selection in the setup panel, it will close
    //  the second selection scrollview. 
    // Clicking an already active main selection button
    //  will close it.
    public void AgentSelectionButtonClicked()
    {
        if (agentScrollView.activeSelf == true)
        {
            agentScrollView.SetActive(false);
        }
        else
        {
            agentScrollView.SetActive(true);
            vehicleScrollView.SetActive(false);
        }
    }
    public void VehicleSelectionButtonClicked()
    {
        if (vehicleScrollView.activeSelf == true)
        {
            vehicleScrollView.SetActive(false);
        }
        else
        {
            vehicleScrollView.SetActive(true);
            agentScrollView.SetActive(false);
        }
    }

    void LoadAgentImageStats(int agentIndex)
    {
        switch (loadedAgents[agentIndex].EfficiencyStars)
        {
            case 1:
                selectedAgentStat1.texture = starImages[0];
                break;
            case 2:
                selectedAgentStat1.texture = starImages[1];
                break;
            case 3:
                selectedAgentStat1.texture = starImages[2];
                break;
            case 4:
                selectedAgentStat1.texture = starImages[3];
                break;
            case 5:
                selectedAgentStat1.texture = starImages[4];
                break;
            default:
                break;
        }

        switch (loadedAgents[agentIndex].CharmStars)
        {
            case 1:
                selectedAgentStat2.texture = starImages[0];
                break;
            case 2:
                selectedAgentStat2.texture = starImages[1];
                break;
            case 3:
                selectedAgentStat2.texture = starImages[2];
                break;
            case 4:
                selectedAgentStat2.texture = starImages[3];
                break;
            case 5:
                selectedAgentStat2.texture = starImages[4];
                break;
            default:
                break;
        }

        switch (loadedAgents[agentIndex].ReliabilityStars)
        {
            case 1:
                selectedAgentStat3.texture = starImages[0];
                break;
            case 2:
                selectedAgentStat3.texture = starImages[1];
                break;
            case 3:
                selectedAgentStat3.texture = starImages[2];
                break;
            case 4:
                selectedAgentStat3.texture = starImages[3];
                break;
            case 5:
                selectedAgentStat3.texture = starImages[4];
                break;
            default:
                break;
        }
    }

    void LoadVehicleImageStats(int vehicleIndex)
    {
        switch (loadedVehicles[vehicleIndex].SpeedStars)
        {
            case 1:
                selectedVehicleStat1.texture = starImages[0];
                break;
            case 2:
                selectedVehicleStat1.texture = starImages[1];
                break;
            case 3:
                selectedVehicleStat1.texture = starImages[2];
                break;
            case 4:
                selectedVehicleStat1.texture = starImages[3];
                break;
            case 5:
                selectedVehicleStat1.texture = starImages[4];
                break;
            default:
                break;
        }

        switch (loadedVehicles[vehicleIndex].CapacityStars)
        {
            case 1:
                selectedVehicleStat2.texture = starImages[0];
                break;
            case 2:
                selectedVehicleStat2.texture = starImages[1];
                break;
            case 3:
                selectedVehicleStat2.texture = starImages[2];
                break;
            case 4:
                selectedVehicleStat2.texture = starImages[3];
                break;
            case 5:
                selectedVehicleStat2.texture = starImages[4];
                break;
            default:
                break;
        }

        switch (loadedVehicles[vehicleIndex].ReliabilityStars)
        {
            case 1:
                selectedVehicleStat3.texture = starImages[0];
                break;
            case 2:
                selectedVehicleStat3.texture = starImages[1];
                break;
            case 3:
                selectedVehicleStat3.texture = starImages[2];
                break;
            case 4:
                selectedVehicleStat3.texture = starImages[3];
                break;
            case 5:
                selectedVehicleStat3.texture = starImages[4];
                break;
            default:
                break;
        }
    }
}
