using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionController : MonoBehaviour
{
    [SerializeField]
    List<Texture> agentImages;
    [SerializeField]
    List<Texture> vehicleImages;
    [SerializeField]
    List<Texture> locationImages;

    [SerializeField]
    GameObject missionSetupPanel;
    [SerializeField]
    GameObject activeMissionButtonPrefab;
    [SerializeField]
    MissionSetupController missionSetupController;

    public Text currentCashText;
    public float currentCash;

    public int missionCounter = 0;

    List<Agent> agents = new List<Agent>();

    public List<Agent> ownedAgents = new List<Agent>();
    public List<Vehicle> ownedVehicles = new List<Vehicle>();

    List<Vehicle> vehicles = new List<Vehicle>();
    [SerializeField]
    List<Mission> activeMissions = new List<Mission>();
    List<Mission> missionsToRemove = new List<Mission>();
    List<int> missionIndexesToRemove = new List<int>();
    List<Location> locations = new List<Location>();
    int startAgentCount = 4;

    public int maxVehicles = 4, maxAgents = 4;

#warning Pierre Change
    float vehicleReliabilityCheckTimer = 10f;
    float reliabilitySecondsToAdd = 2f;

    float baseMissionTime = 60f;

#warning Ryan Change
    public AudioSource Cash;
    public AudioSource Skip;
    public AudioSource Arrest;
    public AudioSource failStart;
    public AudioSource missionStart;
    public AudioSource missionDelay;
    //bool passed = false;

    [SerializeField]
    GameObject cam;

    [SerializeField]
    List<GameObject> travelingVehicles;

    [SerializeField]
    GameObject LogMissionGameobject;
    [SerializeField]
    GameObject PF_logOfMission;
    List<GameObject> LoggedMissions = new List<GameObject>();
    private void Awake()
    {
        agents.Add(new Agent(1, 1, 1, "John BlackHat", agentImages[0], Agent.AgentStatus.Standby));
        agents.Add(new Agent(2, 2, 2, "Miller Vandermouse", agentImages[1], Agent.AgentStatus.InShop));
        agents.Add(new Agent(2, 1, 4, "Blake Phedora Asmodeous the Third", agentImages[2], Agent.AgentStatus.InShop));
        agents.Add(new Agent(3, 5, 1, "Craig List", agentImages[3], Agent.AgentStatus.InShop));
        agents.Add(new Agent(3, 3, 3, "John Doe", agentImages[4], Agent.AgentStatus.InShop));
        agents.Add(new Agent(5, 5, 5, "Aaron Red", agentImages[5], Agent.AgentStatus.InShop));


        vehicles.Add(new Vehicle(1, 1, 1, 100f, "The Junker", vehicleImages[0], Color.gray, Vehicle.VehicleStatus.Standby));
        vehicles.Add(new Vehicle(5, 1, 2, 500f, "The Turbo", vehicleImages[1], Color.blue, Vehicle.VehicleStatus.InShop));
        vehicles.Add(new Vehicle(3, 3, 4, 100f, "The Hellion", vehicleImages[2], Color.white, Vehicle.VehicleStatus.InShop));
#warning Pierre Change
        // Changed max capacity value (now 4)
        vehicles.Add(new Vehicle(2, 4, 4, 100f, "The Armada", vehicleImages[3], Color.red, Vehicle.VehicleStatus.InShop));
        vehicles.Add(new Vehicle(3, 5, 3, 100f, "The Big Rig", vehicleImages[4], Color.green, Vehicle.VehicleStatus.InShop));
#warning Pierre Change
        // Changed max capacity value (now 4)
        vehicles.Add(new Vehicle(5, 4, 5, 100f, "Golden Van", vehicleImages[5], Color.yellow /*Gold*/, Vehicle.VehicleStatus.InShop));

        locations.Add(new Location("Northwald", locationImages[0], 1)); // Location Index 0
        locations.Add(new Location("Hollowvale", locationImages[1], 3)); // Location Index 1
        locations.Add(new Location("Ironoak", locationImages[2], 1)); // 2
        locations.Add(new Location("Lorden", locationImages[3], 1)); // 3
        locations.Add(new Location("Fogland", locationImages[4], 2)); // 4
        locations.Add(new Location("Eriview", locationImages[5], 1)); // 5
        locations.Add(new Location("Oldfall", locationImages[6], 1)); // 6
        currentCashText.text = "$10000";
        currentCash = 10000;
    }

    // Use this for initialization
    void Start()
    {
        // Ensure all of our default agents are added to the ownedAgents list
        foreach (Agent agent in agents)
        {
            if (agent.myStatus == Agent.AgentStatus.Standby)
            {
                ownedAgents.Add(agent);
            }
        }

        // Ensure all of our default vehicles are added to the ownedVehicles list
        foreach (Vehicle vehicle in vehicles)
        {
            if (vehicle.myStatus == Vehicle.VehicleStatus.Standby)
            {
                ownedVehicles.Add(vehicle);
            }
        }

        GetComponent<SaveLoadController>().SaveCurrentState(currentCash, ownedAgents, activeMissions);
    }

    // Update is called once per frame
    void Update()
    {
        currentCashText.text = "$" + currentCash;
#warning alex change - moved code here to ToolBarScript. fixes the interaction with locations.


        // Game Over Logic
        // If we have less than 1300 cash
//#warning alex patch - nov 26. patches the problem that if we send an agent out and use all of our remaining cash we don't fail untill the agent is caught
//        if (currentCash < 1300 && activeMissions.Count == 0)
//        {
//            // Then we cannot afford to run a mission with 
//            // the cheapest mission, the cheapest vehicle and the cheapest agent
//            // Therefore we lose.
//            GameOver();
//        }

        // If we have no agents or no vehicles
        //  and we cannot afford to buy the cheapest agent or vehicle ($6000)
        //  and cannot aford to run the cheapest mission afterwards ($1300 for a 1kg run across 1 border)
        if ((ownedAgents.Count <= 0 || ownedVehicles.Count <= 0) && currentCash < 12000 + 200 && activeMissions.Count == 0)
        {
            // Then we lose.
            GameOver();
        }


        //Debug.Log(activeMissions.Count);
        foreach (Mission mission in activeMissions)
        {
            int Index = activeMissions.IndexOf(mission);
            Debug.Log(/*"Start Time: " + mission.startTime + "    End Time: " + mission.endTime*/ "mission Time remaining: " + mission.missionTime.ToString("000"));

#warning Pierre Change
            vehicleReliabilityCheckTimer -= Time.deltaTime;
            if (vehicleReliabilityCheckTimer <= 0)
            {
                vehicleReliabilityCheckTimer = 5f;

                // If we fail the reliability check
                if (VehicleReliabilityCheck(mission.assignedVehicle) == false)
                {
                    Debug.Log("Vehicle failed reliability check. Added " + reliabilitySecondsToAdd + " seconds to mission timer.");
                    mission.missionTime += reliabilitySecondsToAdd;
                    missionDelay.Play();

                }
            }

            //mission.progressTime += Time.deltaTime;
            mission.missionTime -= Time.deltaTime;

            LoggedMissions[Index].transform.GetChild(3).GetComponent<Text>().text = mission.missionTime.ToString("000");//the mission progress child

            // If the minigame associated to this mission passed and the menu is closed
            if (mission.minigameSuccess.HasValue && mission.minigameSuccess.Value == true && !GetComponent<roadBlockQuestions>().showGUI)
            {
                // Tell it we managed to cross a border
                mission.crossedBorders++;

                //mission.startTime = Time.time;
                //mission.endTime = Time.time + baseMissionTime - mission.assignedAgent.EfficiencySecondsRemoved - mission.assignedVehicle.SpeedSecondsRemoved;
                mission.missionTime = baseMissionTime - mission.assignedAgent.EfficiencySecondsRemoved - mission.assignedVehicle.SpeedSecondsRemoved;
                // Prevent this from happening twice in a row.
                mission.minigameSuccess = null;
            }

            // If we crossed all the borders
            if (mission.crossedBorders >= mission.assignedLocation.bordersToCross + 1)
            {
                // Add to list of mission indexes that need to be removed
                missionIndexesToRemove.Add(activeMissions.IndexOf(mission));
                mission.missionSuccess = true;
            }

            if (mission.missionTime <= 0 && !gameObject.GetComponent<roadBlockQuestions>().showGUI
                && mission.crossedBorders < mission.assignedLocation.bordersToCross && !mission.missionSuccess.HasValue)
            {
                // (Base Risk + Extra Risk) – Agent Charm – Vehicle Reliability = Fail%

#warning Pierre Change
                /*
                 * Agent Reliability Check
                 * Checks to see if the agent will give themselves into the police
                 * 
                 * If they do, then we fail the mission, and remove the agent from our
                 *  owned agents list, and then change their status to InShop.
                 *  
                 * If they don't, then we roll a random number to see if we will 
                 *  play the minigame or not
                 */

#warning Pierre Change
                // If we pass the reliability check
                if (AgentReliabilityCheck(mission.assignedAgent) == true)
                {
                    int rdn = Random.Range(0, 101);

                    if (missionCounter > 3)
                    {

                        if (rdn > (50 + 0) - mission.assignedAgent.CharmPercentages - mission.assignedVehicle.ReliabilityPercentages)
                        {
                            if (!gameObject.GetComponent<roadBlockQuestions>().showGUI)
                            {
                                gameObject.GetComponent<roadBlockQuestions>().StartMiniGame(activeMissions.IndexOf(mission), mission.assignedAgent.AgentName);
                            }
                        }
                        else
                        {
                            Debug.Log("Skipped Checkpoint");
                            Skip.Play();
                            mission.minigameSuccess = true;
                        }
                    }
                    else
                    {
                        if (!gameObject.GetComponent<roadBlockQuestions>().showGUI)
                        {
                            gameObject.GetComponent<roadBlockQuestions>().StartMiniGame(activeMissions.IndexOf(mission), mission.assignedAgent.AgentName);
                        }
                    }
                }
#warning Pierre Change
                // If we fail the reliability check
                else
                {
                    Debug.Log("Agent Failed Reliability Check. Removed from owned agents and mission failed.");
                    mission.minigameSuccess = false;
                    FailedMission(activeMissions.IndexOf(mission));
                }
            }
            else if (mission.crossedBorders >= mission.assignedLocation.bordersToCross &&
                        mission.missionTime <= 0)
            {
                mission.minigameSuccess = true;
            }
        }

        // This is here to prevent removing from activeMissions while iterating through it
        while (missionIndexesToRemove.Count > 0)
        {
            Destroy(LoggedMissions[missionIndexesToRemove[missionIndexesToRemove.Count - 1]], 0.1f);
            LoggedMissions.RemoveAt(missionIndexesToRemove[missionIndexesToRemove.Count - 1]);

            //checks to see if the mission has been completed (null if not complete = fails check), then if it has succeeded the mission. 
            if (activeMissions[missionIndexesToRemove[missionIndexesToRemove.Count - 1]].missionSuccess.HasValue ?
                activeMissions[missionIndexesToRemove[missionIndexesToRemove.Count - 1]].missionSuccess.Value : false)
            {
                currentCash += activeMissions[missionIndexesToRemove[missionIndexesToRemove.Count - 1]].profit;
                Cash.Play();
#warning alex change nov 26.
                activeMissions[missionIndexesToRemove[missionIndexesToRemove.Count - 1]].ReturnAgentsAndVehicles();

            }
#warning alex change nov 26.
            else
            {
                activeMissions[missionIndexesToRemove[missionIndexesToRemove.Count - 1]].LoseAgentsAndVehicles();
            }
            Debug.Log(activeMissions[missionIndexesToRemove[missionIndexesToRemove.Count - 1]].missionSuccess);
            activeMissions.RemoveAt(missionIndexesToRemove[missionIndexesToRemove.Count - 1]);
            missionIndexesToRemove.RemoveAt(missionIndexesToRemove.Count - 1);
        }
        //loops through the number of missions and positions the gamobject (picture) accordingly
        for (int i = 0; i < travelingVehicles.Count; i++)
        {
            if (i >= activeMissions.Count)
            {
                travelingVehicles[i].SetActive(false);
                i = travelingVehicles.Count;
            }
            else
            {
                travelingVehicles[i].SetActive(true);
                //the total distance, the last position, time, normalized vector
                int crossed = activeMissions[i].crossedBorders;
                int ToCross = activeMissions[i].travelPoints.Count;
                float theDiff = Vector3.Distance(activeMissions[i].travelPoints[ToCross - crossed - 1], activeMissions[i].travelPoints[ToCross - crossed - 2]);
                Vector3 direction = Vector3.Normalize(activeMissions[i].travelPoints[ToCross - crossed - 1] - activeMissions[i].travelPoints[ToCross - crossed - 2]);
                travelingVehicles[i].transform.position = activeMissions[i].travelPoints[ToCross - crossed - 2] + direction * (activeMissions[i].missionTime / activeMissions[i].totalMissionTime) * theDiff;

                direction = Vector3.Normalize(activeMissions[i].travelPoints[ToCross - crossed - 2] - activeMissions[i].travelPoints[ToCross - crossed - 1]);
                travelingVehicles[i].transform.GetChild(0).rotation = Quaternion.LookRotation(Vector3.back, direction);
            }
        }
    }

    // Called when the confirm button is pressed
    public void StartMission(int agentIndex, int vehicleIndex, Location locationIndex, List<Vector3> path)
    {
        float weight = missionSetupController.weightSlider.value;
        Agent agent = agents[agentIndex];
        Vehicle vehicle = vehicles[vehicleIndex];
        Location location = locationIndex;

        Mission newMission = new Mission(weight, agent, vehicle, location, path);
        //newMission.startTime = Time.time;

        if (newMission.totalCost <= currentCash)
        {
            missionStart.Play();
            currentCash -= newMission.totalCost;

            agent.myStatus = Agent.AgentStatus.OnMission;
            vehicle.myStatus = Vehicle.VehicleStatus.OnMission;

            // Misions start at 60 seconds and we remove agent and vehicle stat buffs
            //newMission.endTime = newMission.startTime + baseMissionTime - agent.EfficiencySecondsRemoved - vehicle.SpeedSecondsRemoved;
            newMission.missionTime = baseMissionTime - agent.EfficiencySecondsRemoved - vehicle.SpeedSecondsRemoved;
            newMission.totalMissionTime = newMission.missionTime;
            activeMissions.Add(newMission);
            missionCounter++;

            GameObject log = Instantiate(PF_logOfMission, LogMissionGameobject.transform);
            log.transform.GetChild(0).GetComponent<RawImage>().texture = location.LocationImage;//this is the location image child
            log.transform.GetChild(1).GetComponent<Text>().text = agent.AgentName;//this is the agent child
            log.transform.GetChild(2).GetComponent<Text>().text = location.LocationName;//this is the location name child
            log.transform.GetChild(3).GetComponent<Text>().text = newMission.missionTime.ToString("000");//this is the mission progress child
            LoggedMissions.Add(log);
        }
        else
        {
            failStart.Play();
        }
    }

    //    public void EndMission(int index)
    //    {
    //#warning To-Do
    //        // Re-activate the agent and vehicle involved in our
    //        //  agent/vehicle menu so we can re-select them

    //        currentCash += activeMissions[index].profit;
    //    }

    //public void TestButton()
    //{
    //    // Test
    //    StartMission(agents[0], vehicles[0], vehicles[0].CapacityMaximums, locations[0]);
    //    gameObject.GetComponent<roadBlockQuestions>().StartMiniGame(0);
    //}

    public void OpenMissionSetup(int locationNumber, List<Vector3> paths)
    {
        missionSetupPanel.SetActive(true);
        missionSetupController.LoadData(agents, vehicles, locations[locationNumber], paths);
    }

    public void PassedMinigame(bool didPass, int missionIndex)
    {
        activeMissions[missionIndex].minigameSuccess = didPass;
        FailedMission(missionIndex);
    }

    public void FailedMission(int missionIndex)
    {
        if (activeMissions[missionIndex].minigameSuccess == false)
        {
            activeMissions[missionIndex].missionSuccess = false;
            // Add to list of mission indexes that need to be removed
            missionIndexesToRemove.Add(missionIndex);


            // Remove the assigned Agent/Vehicle from our owned lists
            ownedAgents.Remove(activeMissions[missionIndex].assignedAgent);
#warning Pierre Change
            ownedVehicles.Remove(activeMissions[missionIndex].assignedVehicle);
            //Play Police Sound
            Arrest.Play();

            // Add them to the shop again
            activeMissions[missionIndex].assignedAgent.myStatus = Agent.AgentStatus.InShop;
            activeMissions[missionIndex].assignedVehicle.myStatus = Vehicle.VehicleStatus.InShop;
        }
    }

    public void AgentBought(float cost, Agent agent)
    {
        currentCash -= cost;
        agent.myStatus = Agent.AgentStatus.Standby;


        // If we don't already own this agent
        if (!ownedAgents.Contains(agent))
        {
            // Add them to the list
            ownedAgents.Add(agent);
        }
        else
        {
            // Otherwise, display an error
            Debug.LogError("Attempted to add an agent to the ownedAgents list when they were already in the list. " +
                "This means we forgot to remove the agent from the list. Location: MissionController AgentBought() method.");
        }
    }


    // Added this method VehicleBought()
    public void VehicleBought(float cost, Vehicle vehicle)
    {
        currentCash -= cost;
        vehicle.myStatus = Vehicle.VehicleStatus.Standby;

        // If we don't already own this vehicle
        if (!ownedVehicles.Contains(vehicle))
        {
            // Add them to the list
            ownedVehicles.Add(vehicle);
        }
        else
        {
            // Otherwise, display an error
            Debug.LogError("Attempted to add a vehicle to the ownedVehicles list when they were already in the list. " +
                "This means we forgot to remove the vehicle from the list. Location: MissionController VehicleBought() method.");
        }
    }

    public void GetAgentsInShop(ref List<Agent> agentList)
    {
        agentList.Clear();

        foreach (Agent obj in agents)
        {
            if (obj.myStatus == Agent.AgentStatus.InShop)
            {
                agentList.Add(obj);
            }
        }
    }

    public int GetAgentsCount()
    {
        return agents.Count;
    }
    public int GetVehiclesCount()
    {
        return vehicles.Count;
    }

    public Agent GetAgentIndex(int i)
    {
        return agents[i];
    }
    public Vehicle GetVehicleIndex(int i)
    {
        return vehicles[i];
    }

    public List<Vehicle> GetVehiclesInShop()
    {
        List<Vehicle> shopVehicles = new List<Vehicle>();

        foreach (Vehicle obj in vehicles)
        {
            if (obj.myStatus == Vehicle.VehicleStatus.InShop)
            {
                shopVehicles.Add(obj);
            }
        }

        return shopVehicles;
    }

    public List<Vehicle> GetVehicles()
    {
        return vehicles;
    }


#warning Pierre Change
    /// <summary>
    /// Returns true if the agent passes the check.
    /// Returns false if the agent auto-fails the mission.
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    bool AgentReliabilityCheck(Agent agent)
    {
        if (missionCounter > 3)
        {
            float rdn = Random.Range(0f, 100f);

            // If we roll lower than the auto-fail percentage
            if (rdn <= agent.ReliabilityPercentages)
            {
                // Then we auto-fail
                return false;
            }
            // If we roll higher than the auto-fail percentage
            else
            {
                // Then we don't auto-fail
                return true;
            }
        }
        else
        {
            return true;
        }
    }

#warning Pierre Change
    /// <summary>
    /// Returns true if the vehicle passes the check.
    /// Returns false if the vehicle fails the check.
    /// </summary>
    /// <param name="vehicle"></param>
    /// <returns></returns>
    bool VehicleReliabilityCheck(Vehicle vehicle)
    {
        float rdn = Random.Range(0f, 100f);

        // If we roll lower than the reliability percentage
        if (rdn <= vehicle.ReliabilityPercentages)
        {
            // Then we fail the check
            return false;
        }
        // If we roll higher than the reliability percentage
        else
        {
            // Then we don't fail the check
            return true;
        }
    }

    // Checks if we can afford this cost
    public bool CanAfford(float cost)
    {
        if (currentCash >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Added a GameOver scene and loaded it upon losing the game
    // Cage image used is free for commercial use and requires no attribution
    // https://pixabay.com/en/cage-bars-cell-jail-prison-1161869/
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }


    //method to load data from the SaveLoadController
    public void LoadDataToController(float cashToLoad, List<Agent> agentsToLoad, List<Mission> missionsToLoad)
    {
        currentCash = cashToLoad;
        ownedAgents = agentsToLoad;
        activeMissions = missionsToLoad;

    }

    public void LoadButton()
    {
        GetComponent<SaveLoadController>().LoadSavedState();
    }
}
