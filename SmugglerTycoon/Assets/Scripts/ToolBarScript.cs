using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarScript : MonoBehaviour {

    //Get Panels
    public RectTransform TaskBar;
    public RectTransform AgentDrop;
    public RectTransform VehicleDrop;
    public RectTransform ShopDrop;
    public RectTransform StatDrop;
    public GameObject missionSetupPanel;

    //Get Text
    public Text CashText;

    //Float Values
    protected float Cash = 0f;


    //Bool Values
    bool agentsDropped = false;
    bool vehicleDropped = false;
    bool shopDropped = false;
    bool statDropped = false;

    enum AgentHover {NotHover, Agent1, Agent2, Agent3, Agent4 };

    AgentHover agentHoverstate = AgentHover.NotHover;

#warning alex change - nov 25
    [SerializeField]
    cameraController camControl;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CashText.text = "$" + Cash;

        switch (agentHoverstate)
        {
            case AgentHover.NotHover:
                break;
            case AgentHover.Agent1:
                break;
            case AgentHover.Agent2:
                break;
            case AgentHover.Agent3:
                break;
            case AgentHover.Agent4:
                break;
            default:
                break;
        }
#warning alex change - fix the ability to click on location when a menu is up - nov 25.
        if (AgentDrop.gameObject.activeSelf || VehicleDrop.gameObject.activeSelf || ShopDrop.gameObject.activeSelf || StatDrop.gameObject.activeSelf || transform.GetChild(6).gameObject.activeSelf || roadBlockQuestions.aRoadBlockIsOn)
        {
            camControl.useInteraction = false;
        }
        else
        {
            camControl.useInteraction = true;
        }
    }


    //Methods for DropDown menus - first 4 are for lowering their dropdown , the last is for raising them all.
    public void DropAgents()
    {
        if(!missionSetupPanel.activeSelf)
        {
            if (!agentsDropped)
            {
                RaiseAllDrops();
                AgentDrop.gameObject.SetActive(true);
            }
            else
            {
                RaiseAllDrops();
            }
            agentsDropped = !agentsDropped;
            statDropped = false;
            vehicleDropped = false;
            shopDropped = false;
        }
    }
    public void DropVehicles()
    {
        if (!missionSetupPanel.activeSelf)
        {

            if (!vehicleDropped)
            {
                RaiseAllDrops();
                VehicleDrop.gameObject.SetActive(true);
            }
            else
            {
                RaiseAllDrops();
            }
            vehicleDropped = !vehicleDropped;
            agentsDropped = false;
            statDropped = false;
            shopDropped = false;
        }
    }
    public void DropShop()
    {
        if (!missionSetupPanel.activeSelf)
        {

            if (!shopDropped)
            {
                RaiseAllDrops();
                ShopDrop.gameObject.SetActive(true);
            }
            else
            {
                RaiseAllDrops();
            }
            shopDropped = !shopDropped;
            agentsDropped = false;
            vehicleDropped = false;
            statDropped = false;
        }
    }
    public void DropStats()
    {
        if (!missionSetupPanel.activeSelf)
        {
            if (!statDropped)
            {
                RaiseAllDrops();
                StatDrop.gameObject.SetActive(true);
            }
            else
            {
                RaiseAllDrops();
            }
            statDropped = !statDropped;
            agentsDropped = false;
            vehicleDropped = false;
            shopDropped = false;
        }
    }
    public void RaiseAllDrops()
    {
        AgentDrop.gameObject.SetActive(false);
        VehicleDrop.gameObject.SetActive(false);
        ShopDrop.gameObject.SetActive(false);
        StatDrop.gameObject.SetActive(false);
        missionSetupPanel.SetActive(false);
    }
}
