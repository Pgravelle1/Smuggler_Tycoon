using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission
{
    public Agent assignedAgent;
    public Vehicle assignedVehicle;
    public Location assignedLocation;
    public float contrabandWeight, riskLevel;
    public float profit,
        investmentCost, // How much it costs to purchase our drugs
        cashReceived,
        agentCut,
        totalCost,
        totalMissionTime,//hold the initial time the mission will take. used to 
        missionTime;//hold the full time, then reduces reductions, -deltatime every update
        //startTime, 
        //endTime, 
        //delayTime, 
        //progressTime; // Time elapsed since start of mission
    public string missionName;
    public RawImage missionImage;
    public GameObject missionPathManager;

    public int crossedBorders = 0;

    public bool? missionSuccess;
    public bool? minigameSuccess;

    internal List<Vector3> travelPoints;
    public Mission(float weight, Agent agent, Vehicle vehicle, Location location, List<Vector3> path)
    {
        contrabandWeight = weight;
        assignedAgent = agent;
        assignedVehicle = vehicle;
        assignedLocation = location;
        travelPoints = path;

        //weight(3 + (0.5 * #OfBordersCrossed)

        // How much it costs to send the drugs without taking anything else into account
        investmentCost = (100 * contrabandWeight);

        // How much it costs to run this mission in total
        totalCost = investmentCost + (vehicle.FuelEconomy * location.bordersToCross);

        // How much money is received from this mission
        cashReceived = contrabandWeight * 100 * (3 + (0.5f * location.bordersToCross));

        // How much money is kept by the agent
        agentCut = cashReceived * agent.Wage;

        // How much of that money we get to keep
        profit = cashReceived - agentCut;

        Debug.Log("Total Cost: " + totalCost + "   Expected Cash Received: " + cashReceived  + "   Agent Cut: " + agentCut + "   Profit: " + profit);
    }
#warning alex changes (next 2 methods) - nov 26;
    internal void ReturnAgentsAndVehicles()
    {
        assignedAgent.myStatus = Agent.AgentStatus.Standby;
        assignedVehicle.myStatus = Vehicle.VehicleStatus.Standby;
    }
    internal void LoseAgentsAndVehicles()
    {
        assignedAgent.myStatus = Agent.AgentStatus.InShop;
        assignedVehicle.myStatus = Vehicle.VehicleStatus.InShop;
    }
}
