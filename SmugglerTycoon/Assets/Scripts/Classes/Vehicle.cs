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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle
{
    //[SerializeField]
    //RawImage m_carImage, m_truckImage, m_vanImage;

    public enum VehicleType
    {
        Car,
        Truck,
        Van
    }

    public enum VehicleStatus
    {
        InShop,
        Standby,
        OnMission,
        Impounded
    }

    public VehicleStatus myStatus;
    #region Member Variables
    private int m_speedStars, // Number of stars the vehicle has in the speed stat
        m_capacityStars, // Number of stars the vehicle has in the capacity stat
        m_reliabilityStars; // Number of stars the vehicle has in the reliability stat

    private string m_name; // The name of the vehicle

    public Texture vehicleImage; // The image of the vehicle

    public float vehicleShopCost;

    private Color m_color; // The color of the vehicle

    //VehicleType vehicleType = new VehicleType(); // The type of the vehicle

    //VehicleStatus myStatus = new VehicleStatus(); // The type of the agent
    #endregion
    // These arrays give us the values for each star
    #region Stat Arrays
    private float[] m_speedSecondsRemoved = new float[5]
    {
        5, // 1 Star 
        10, // 2 Stars
        15, // 3 Stars
        20, // 4 Stars
        25  // 5 Stars
    };

    private float[] m_capacityMaximums = new float[5]
    {
        #warning Pierre Change
        10,  // 1 Star  (2kg)
        20,  // 2 Stars (4kg)
        50,  // 3 Stars (6kg)
        100,  // 4 Stars (8kg)
        1000, // 5 Stars (10kg)
    };

    private float[] m_reliabilityPercentages = new float[5]
    {
        #warning Pierre Change
        20, // 1 Star   (30% chance of breaking down)
        15, // 2 Stars  (25% chance of breaking down)
        10, // 3 Stars  (20% chance of breaking down)
        5, // 4 Stars  (10% chance of breaking down)
        100,  // 5 Stars  (0% chance of breaking down)
    };

    private float m_fuelEconomyCost; // The cost of crossing a border.
    #endregion

    

    #region Properties
    public int SpeedStars
    {
        get
        {
            return m_speedStars;
        }
    }

    public int CapacityStars
    {
        get
        {
            return m_capacityStars;
        }
    }

    public int ReliabilityStars
    {
        get
        {
            return m_reliabilityStars;
        }
    }

    public float FuelEconomy
    {
        get
        {
            return m_fuelEconomyCost;
        }
    }

    public float SpeedSecondsRemoved
    {
        get
        {
            return m_speedSecondsRemoved[m_speedStars - 1];
        }
    }

    public float CapacityMaximums
    {
        get
        {
            return m_capacityMaximums[m_capacityStars - 1];
        }
    }

    public float ReliabilityPercentages
    {
        get
        {
            return m_reliabilityPercentages[m_reliabilityStars - 1];
        }
    }

    public string VehicleName
    {
        get
        {
            return m_name;
        }
    }

    public Color VehicleColor
    {
        get
        {
            return m_color;
        }
    }

    #endregion

    /// <summary>
    /// Creates a vehicle using the given number of stars for each stat, its cost per border crossed, 
    /// its name, its vehicle type, and its color.
    /// </summary>
    /// <param name="speedStars">
    /// The number of stars in the speed stat. (Must be an int from 1 to 5)
    /// </param>
    /// <param name="capacityStars">
    /// The number of stars in the speed stat. (Must be an int from 1 to 5)
    /// </param>
    /// <param name="reliabilityStars">
    /// The number of stars in the speed stat. (Must be an int from 1 to 5)
    /// </param>
    /// <param name="fuelEconomyCost">
    /// The cost of crossing a single border.
    /// </param>
    /// <param name="vehicleName">
    /// The name of this vehicle.
    /// </param>
    /// <param name="type">
    /// The vehicle type. Use Vehicle.VehicleType.
    /// </param>
    /// <param name="color">
    /// The color this vehicle's image will be.
    /// </param>
    public Vehicle(int speedStars, int capacityStars, int reliabilityStars, 
        float fuelEconomyCost, string vehicleName, Texture image, Color color, VehicleStatus status)
    {
        if (speedStars <= 5 && speedStars > 0 &&
            capacityStars <= 5 && capacityStars > 0 &&
            reliabilityStars <= 5 && reliabilityStars > 0 &&
            fuelEconomyCost > 0 &&
            !String.IsNullOrEmpty(vehicleName.Trim()))
        {
            m_speedStars = speedStars;
            m_capacityStars = capacityStars;
            m_reliabilityStars = reliabilityStars;
            m_fuelEconomyCost = fuelEconomyCost;
            m_name = vehicleName;
            myStatus = status;
            m_color = color;

            int totalStars = m_speedStars + m_capacityStars + m_reliabilityStars;

            vehicleShopCost = totalStars * 2000;
            //myStatus = VehicleStatus.Standby;
            vehicleImage = image;
//            switch (type)
//            {
//#warning Fix Later
//                case VehicleType.Car:
//                    vehicleImage = 
//                    //vehicleImage = m_carImage;
//                    break;
//                case VehicleType.Truck:
//                    //vehicleImage = m_truckImage;
//                    break;
//                case VehicleType.Van:
//                    //vehicleImage = m_vanImage;
//                    break;
//                default:
//                    break;
//            }
        }
        else
        {
            throw new ArgumentOutOfRangeException("A Star value is out of range, or the Fuel Economy Cost is below 0, or the vehicle name is empty or null.");
        }
    }

}
