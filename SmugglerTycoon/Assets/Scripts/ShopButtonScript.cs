using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonScript : MonoBehaviour
{
    ShopManager manager;
    MissionController missionController;
    public Agent myAgent;
    public Vehicle myVehicle;

    Text costText, nameText;
    RawImage agentImage, vehicleImage; // Image of our agent/vehicle displayed on the button
    RawImage statStarsImageLeft, statStarsImageCenter, statStarsImageRight; // Image of stars depending on our stats

    public List<Texture> starImages = new List<Texture>();

   

    public enum ButtonType
    {
        Agent,
        Vehicle
    }

    public ButtonType buttonType;


    private void Awake()
    {
        missionController = GameObject.FindGameObjectWithTag("MissionController").GetComponent<MissionController>();

        if (buttonType == ButtonType.Agent)
        {
            agentImage = transform.GetChild(1).GetComponent<RawImage>();
        }
        else if (buttonType == ButtonType.Vehicle)
        {
            vehicleImage = transform.GetChild(1).GetComponent<RawImage>();
        }
        statStarsImageLeft = transform.GetChild(2).GetComponent<RawImage>();
        statStarsImageCenter = transform.GetChild(3).GetComponent<RawImage>();
        statStarsImageRight = transform.GetChild(4).GetComponent<RawImage>();

        manager = gameObject.GetComponentInParent<ShopManager>();

        starImages = Resources.LoadAll<Texture>("StarImages").ToList<Texture>();

        if (buttonType == ButtonType.Agent)
        {
            nameText = transform.GetChild(0).GetComponent<Text>();
            costText = transform.Find("Cost").GetComponent<Text>();
        }
        else if (buttonType == ButtonType.Vehicle)
        {
            nameText = transform.GetChild(0).GetComponent<Text>();
            costText = transform.Find("Cost").GetComponent<Text>();
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonEvent()
    {

        // Added an if statement to check if we can afford the agent/vehicle
        //  and code to determine which button we're pressing, 
        //  an agent button or vehicle button?
        // Also added methods that check if can afford this agent/vehicle
        //  before committing to it
        switch (buttonType)
        {
            case ButtonType.Agent:
                // If we can afford this agent and we don't have the agent limit
                if(manager.CanAfford(myAgent.agentShopCost) && missionController.ownedAgents.Count != missionController.maxAgents)
                {
                    manager.AgentBought(myAgent.agentShopCost, myAgent);
                    gameObject.GetComponent<Button>().interactable = false;
                }
                else
                {
                    #warning *BUZZ* sound to say we cannot afford this agent
                }
                break;
            case ButtonType.Vehicle:
                // If we can afford this vehicle and we don't have the vehicle limit
                if (manager.CanAfford(myVehicle.vehicleShopCost) && missionController.ownedVehicles.Count != missionController.maxVehicles)
                {
                    manager.VehicleBought(myVehicle.vehicleShopCost, myVehicle);
                    gameObject.GetComponent<Button>().interactable = false;
                }
                else
                {
                    #warning *BUZZ* sound to say we cannot afford this agent
                }

                break;
            default:
                break;
        }
    }

    public void SetAgent(Agent agent)
    {
        myAgent = agent;
        agentImage.texture = agent.agentImage;
        nameText.text = myAgent.AgentName;
        costText.text = string.Format("${0}", myAgent.agentShopCost);

        switch (agent.EfficiencyStars)
        {
            case 1:
                statStarsImageLeft.texture = starImages[0];
                break;
            case 2:
                statStarsImageLeft.texture = starImages[1];
                break;
            case 3:
                statStarsImageLeft.texture = starImages[2];
                break;
            case 4:
                statStarsImageLeft.texture = starImages[3];
                break;
            case 5:
                statStarsImageLeft.texture = starImages[4];
                break;
            default:
                break;
        }

        switch (agent.CharmStars)
        {
            case 1:
                statStarsImageCenter.texture = starImages[0];
                break;
            case 2:
                statStarsImageCenter.texture = starImages[1];
                break;
            case 3:
                statStarsImageCenter.texture = starImages[2];
                break;
            case 4:
                statStarsImageCenter.texture = starImages[3];
                break;
            case 5:
                statStarsImageCenter.texture = starImages[4];
                break;
            default:
                break;
        }

        switch (agent.ReliabilityStars)
        {
            case 1:
                statStarsImageRight.texture = starImages[0];
                break;
            case 2:
                statStarsImageRight.texture = starImages[1];
                break;
            case 3:
                statStarsImageRight.texture = starImages[2];
                break;
            case 4:
                statStarsImageRight.texture = starImages[3];
                break;
            case 5:
                statStarsImageRight.texture = starImages[4];
                break;
            default:
                break;
        }
    }

    public void SetVehicle(Vehicle vehicle)
    {
        myVehicle = vehicle;
        vehicleImage.color = vehicle.VehicleColor;
        vehicleImage.texture = vehicle.vehicleImage;
        nameText.text = myVehicle.VehicleName;
        costText.text = string.Format("${0}", myVehicle.vehicleShopCost);

        switch (vehicle.SpeedStars)
        {
            case 1:
                statStarsImageLeft.texture = starImages[0];
                break;
            case 2:
                statStarsImageLeft.texture = starImages[1];
                break;
            case 3:
                statStarsImageLeft.texture = starImages[2];
                break;
            case 4:
                statStarsImageLeft.texture = starImages[3];
                break;
            case 5:
                statStarsImageLeft.texture = starImages[4];
                break;
            default:
                break;
        }

        switch (vehicle.CapacityStars)
        {
            case 1:
                statStarsImageCenter.texture = starImages[0];
                break;
            case 2:
                statStarsImageCenter.texture = starImages[1];
                break;
            case 3:
                statStarsImageCenter.texture = starImages[2];
                break;
            case 4:
                statStarsImageCenter.texture = starImages[3];
                break;
            case 5:
                statStarsImageCenter.texture = starImages[4];
                break;
            default:
                break;
        }

        switch (vehicle.ReliabilityStars)
        {
            case 1:
                statStarsImageRight.texture = starImages[0];
                break;
            case 2:
                statStarsImageRight.texture = starImages[1];
                break;
            case 3:
                statStarsImageRight.texture = starImages[2];
                break;
            case 4:
                statStarsImageRight.texture = starImages[3];
                break;
            case 5:
                statStarsImageRight.texture = starImages[4];
                break;
            default:
                break;
        }
    }
}
