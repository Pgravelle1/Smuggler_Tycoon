#region Class Info
/***************************************************************
 Class created by Pierre Gravelle 
 November 9th 2017 @ 12pm

 Class Description:
    This class will define the functionality for each vehicle in 
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

#region Vehicle Stat Descriptions
/***********************************************************
                    Vehicle Stat Descriptions:
 ***********************************************************
 Speed: Reduces the amount of time it takes to complete a mission. 
        (5-Star Based Stat)
        i.e: 2-Star so reduces mission time by 10 seconds
 Capacity: The maximum kg's of contraband that this vehicle can carry. 
           The more contraband you carry, the bigger profit you can earn.
            (5-Star Based Stat)
            i.e.: 1-Star so only 2kg's of contraband can be carried.
 Reliability: The percentage chance that the vehicle will break down, adds time to the mission
                if the random number generated is within the percentage range of this stat.
                (5-Star Based Stat)
                i.e: 1-Star so 30% chance of breaking down.
                     Random Number Generation that is <=30 = Add x Seconds
                     Random Number Generation that is >30 = Don't add any seconds
 Fuel Economy: The cost of crossing a border.
               (Number Based Stat)
               i.e: $100
                    so if we cross two borders to get to our mission destination
                    then it will cost another $200
*/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location
{
    //[SerializeField]
    //Texture[] m_locationImages; // All possible location images 

    Texture locationImage; // The actual image of the location

    string locationName; // The name of the location
    string desc; // The actual description of the locaiton
    public int bordersToCross;

    string[] descriptions = new string[] // The possible descriptions
    {
        "A tropical paridise."
    };

    public string LocationName
    {
        get
        {
            return locationName;
        }
    }

    public Texture LocationImage
    {
        get
        {
            return locationImage;
        }
    }

    private void Awake()
    {

    }

    public Location(string name, Texture locationImage, int numBorders)
    {
        bordersToCross = numBorders;
        locationName = name;
        this.locationImage = locationImage;

        
        // I removed the "Uncomment later and locationImage code since it is irrelevant
        // Here it is just in case:
        // locationImage = m_locationImages[Random.Range(0, m_locationImages.Length)];

        desc = descriptions[Random.Range(0, descriptions.Length)];
    }

}
