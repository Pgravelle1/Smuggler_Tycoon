using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPAgentVehicleDropdownController : MonoBehaviour {

    MissionController GameData;

    public GameObject agent1, agent2, agent3, agent4;
    public GameObject vehicle1, vehicle2, vehicle3, vehicle4;
    // Use this for initialization
    void Start ()
    {
		GameData = GameObject.FindGameObjectWithTag("MissionController").GetComponent<MissionController>();
    }
	// Update is called once per frame
	void Update ()
    {
        // Agents
        if (GameData.ownedAgents.Count > 0)
        {
            agent1.SetActive(true);
            agent1.GetComponent<RPAgentPanelScript>()
                .PassAgentInfo(GameData.ownedAgents[0].EfficiencyStars, GameData.ownedAgents[0].CharmStars, GameData.ownedAgents[0].ReliabilityStars, GameData.ownedAgents[0].agentImage, GameData.ownedAgents[0].AgentName);
        }
        else
        {
            agent1.SetActive(false);
        }
        if (GameData.ownedAgents.Count > 1)
        {
            agent2.SetActive(true);
            agent2.GetComponent<RPAgentPanelScript>()
                .PassAgentInfo(GameData.ownedAgents[1].EfficiencyStars, GameData.ownedAgents[1].CharmStars, GameData.ownedAgents[1].ReliabilityStars, GameData.ownedAgents[1].agentImage, GameData.ownedAgents[1].AgentName);
        }
        else
        {
            agent2.SetActive(false);
        }
        if (GameData.ownedAgents.Count > 2)
        {
            agent3.SetActive(true);
            agent3.GetComponent<RPAgentPanelScript>()
                .PassAgentInfo(GameData.ownedAgents[2].EfficiencyStars, GameData.ownedAgents[2].CharmStars, GameData.ownedAgents[2].ReliabilityStars, GameData.ownedAgents[2].agentImage, GameData.ownedAgents[2].AgentName);
        }
        else
        {
            agent3.SetActive(false);
        }
        if (GameData.ownedAgents.Count > 3)
        {
            agent4.SetActive(true);
            agent4.GetComponent<RPAgentPanelScript>()
                .PassAgentInfo(GameData.ownedAgents[3].EfficiencyStars, GameData.ownedAgents[3].CharmStars, GameData.ownedAgents[3].ReliabilityStars, GameData.ownedAgents[3].agentImage, GameData.ownedAgents[3].AgentName);
        }
        else
        {
            agent4.SetActive(false);
        }


        // Vehicles
        if (GameData.ownedVehicles.Count > 0)
        {
            vehicle1.SetActive(true);
            vehicle1.GetComponent<RPVehiclePanelScript>()
                .PassVehicleInfo(GameData.ownedVehicles[0].SpeedStars, GameData.ownedVehicles[0].CapacityStars, GameData.ownedVehicles[0].ReliabilityStars, GameData.ownedVehicles[0].vehicleImage, GameData.ownedVehicles[0].VehicleName, GameData.ownedVehicles[0].VehicleColor);
        }
        else
        {
            vehicle1.SetActive(false);
        }
        if (GameData.ownedVehicles.Count > 1)
        {
            vehicle2.SetActive(true);
            vehicle2.GetComponent<RPVehiclePanelScript>()
                .PassVehicleInfo(GameData.ownedVehicles[1].SpeedStars, GameData.ownedVehicles[1].CapacityStars, GameData.ownedVehicles[1].ReliabilityStars, GameData.ownedVehicles[1].vehicleImage, GameData.ownedVehicles[1].VehicleName, GameData.ownedVehicles[1].VehicleColor);
        }
        else
        {
            vehicle2.SetActive(false);
        }
        if (GameData.ownedVehicles.Count > 2)
        {
            vehicle3.SetActive(true);
            vehicle3.GetComponent<RPVehiclePanelScript>()
                .PassVehicleInfo(GameData.ownedVehicles[2].SpeedStars, GameData.ownedVehicles[2].CapacityStars, GameData.ownedVehicles[2].ReliabilityStars, GameData.ownedVehicles[2].vehicleImage, GameData.ownedVehicles[2].VehicleName, GameData.ownedVehicles[2].VehicleColor);
        }
        else
        {
            vehicle3.SetActive(false);
        }
        if (GameData.ownedVehicles.Count > 3)
        {
            vehicle4.SetActive(true);
            vehicle4.GetComponent<RPVehiclePanelScript>()
                .PassVehicleInfo(GameData.ownedVehicles[3].SpeedStars, GameData.ownedVehicles[3].CapacityStars, GameData.ownedVehicles[3].ReliabilityStars, GameData.ownedVehicles[3].vehicleImage, GameData.ownedVehicles[3].VehicleName, GameData.ownedVehicles[3].VehicleColor);
        }
        else
        {
            vehicle4.SetActive(false);
        }
    }

    


}
