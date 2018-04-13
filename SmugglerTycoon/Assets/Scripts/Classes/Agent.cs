#region Class Info
/***************************************************************
 Class created by Pierre Gravelle 
 November 9th 2017 @ 12pm

 Class Description:
    This class will define the functionality for each agent in 
    Team Voyage's border-themed game from the
    NAIT Game Jam (November 2017).

 Terms of Use:
    This class is only to be used for the border-themed game that 
    Team Voyage is creating for the NAIT Game Development Club 
    game jam of November 2017.

 If you would like this class for another game please ask me first.
 Email: Darkforge317@gmail.com
****************************************************************/
#endregion

#region Agent Stat Descriptions
/***********************************************************
                    agent Stat Descriptions:
 ***********************************************************
 Efficiency: Reduces the amount of time it takes to complete a mission. 
        (5-Star Based Stat)

 Charm: Reduces the risk percentage of getting stopped at a border. 
        In other words, a higher charm level will increase the chance
        of an agent skipping a border minigame.

 Reliability: The percentage chance that the agent will give into the police, 
                fails the mission and removes the agents from our owned agents.

 Wage: The percentage of the final profits that the agent will take for themselves.
*/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agent
{ 
    //RawImage[] profileImageList = new RawImage[4];

    public enum AgentStatus
    {
        InShop,
        Standby,
        OnMission
    }

    #region Member Variables
    private int m_efficiencyStars, // Number of stars the agent has in the speed stat
        m_charmStars, // Number of stars the agent has in the capacity stat
        m_reliabilityStars; // Number of stars the agent has in the reliability stat

    private string m_name; // The name of the agent

    public Texture agentImage; // The image of the agent

    public float agentShopCost;

    private Color m_color; // The color of the agent

    public AgentStatus myStatus = new AgentStatus(); // The type of the agent

    // These arrays give us the values for each star
    #region Stat Arrays
    private float[] m_efficiencySecondsRemoved = new float[5]
    {
        5, // 1 Star  
        10, // 2 Stars 
        15, // 3 Stars 
        20, // 4 Stars
        25  // 5 Stars
    };

    private float[] m_charmPercentages = new float[5]
    {
        5,  // 1 Star 
        7,  // 2 Stars
        10,  // 3 Stars 
        15,  // 4 Stars
        20, // 5 Stars
    };

    private float[] m_reliabilityPercentages = new float[5]
    {
        #warning Pierre Change
        20, // 1 Star   (20% chance of auto-fail)
        15, // 2 Stars  (15% chance of auto-fail)
        10, // 3 Stars  (10% chance of auto-fail)
        5, // 4 Stars  (5% chance of auto-fail)
        0,  // 5 Stars  (0% chance of auto-fail)
    };

    private float m_cutPercentage; // The cost (as cut percentage) of sending the agent
    #endregion

    #endregion

    #region Properties
    public int EfficiencyStars
    {
        get
        {
            return m_efficiencyStars;
        }
    }

    public int CharmStars
    {
        get
        {
            return m_charmStars;
        }
    }

    public int ReliabilityStars
    {
        get
        {
            return m_reliabilityStars;
        }
    }

    public float Wage
    {
        get
        {
            m_cutPercentage = (float) (ReliabilityStars + CharmStars + EfficiencyStars) / 100f;
            return m_cutPercentage;
        }
    }

    public float EfficiencySecondsRemoved
    {
        get
        {
            return m_efficiencySecondsRemoved[m_efficiencyStars - 1];
        }
    }

    public float CharmPercentages
    {
        get
        {
            return m_charmPercentages[m_charmStars - 1];
        }
    }

    public float ReliabilityPercentages
    {
        get
        {
            return m_reliabilityPercentages[m_reliabilityStars - 1];
        }
    }

    public string AgentName
    {
        get
        {
            return m_name;
        }
    }
    #endregion

    /// <summary>
    /// Creates a agent using the given number of stars for each stat, its cost per border crossed, 
    /// its name, its agent type, and its color.
    /// </summary>
    /// <param name="efficiencyStars">
    /// The number of stars in the speed stat. (Must be an int from 1 to 5)
    /// </param>
    /// <param name="charmStars">
    /// The number of stars in the speed stat. (Must be an int from 1 to 5)
    /// </param>
    /// <param name="nervousnessStars">
    /// The number of stars in the speed stat. (Must be an int from 1 to 5)
    /// </param>
    /// <param name="wage">
    /// The cost of crossing a single border.
    /// </param>
    /// <param name="type">
    /// The agent type. Use agent.agentType.
    /// </param>
    /// <param name="color">
    /// The color this agent's image will be.
    /// </param>
    public Agent(int efficiencyStars, int charmStars, int nervousnessStars, 
        string agentName, Texture image, AgentStatus status)
    {
        if (efficiencyStars <= 5 && efficiencyStars > 0 &&
            charmStars <= 5 && charmStars > 0 &&
            nervousnessStars <= 5 && nervousnessStars > 0 &&
            !String.IsNullOrEmpty(agentName.Trim()))
        {
            agentImage = image;
            m_efficiencyStars = efficiencyStars;
            m_charmStars = charmStars;
            m_reliabilityStars = nervousnessStars;
            m_name = agentName;
            myStatus = status;

            int totalStars = m_efficiencyStars + m_charmStars + m_reliabilityStars;

            agentShopCost = totalStars * 2000;

            m_cutPercentage = totalStars;
        }
        else
        {
            throw new ArgumentOutOfRangeException("A Star value is out of range, or the wage Cost is below 0, or the agent name is empty or null.");
        }
    }

    //public void LoadProfileImages()
    //{
    //    profileImageList[0] = m_profile1;
    //    profileImageList[1] = m_profile2;
    //    profileImageList[2] = m_profile3;
    //    profileImageList[3] = m_profile4;
    //}
}
