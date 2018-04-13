using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadController : MonoBehaviour {

    static readonly string SAVE_FILE = "player.json";
    string filename;

    private void Awake()
    {
        filename = Path.Combine(Application.persistentDataPath, SAVE_FILE);
    }
    /// <summary>
    /// method to save the current state
    /// </summary>
    /// <param name="CurrentCash"></param>
    /// <param name="CurrentAgents"></param>
    /// <param name="CurrentMissions"></param>
    public void SaveCurrentState (float CurrentCash, List<Agent> CurrentAgents, List<Mission> CurrentMissions) {
        SaveData data = new SaveData()
        {
            ownedAgents = CurrentAgents, currentCash = CurrentCash, activeMissions = CurrentMissions

        };

        string json = JsonUtility.ToJson(data);

        

        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
        File.WriteAllText(filename, json);
        Debug.Log("saved to" + filename);

        string jsonFromFile = File.ReadAllText(filename);

        SaveData copy = JsonUtility.FromJson<SaveData>(jsonFromFile);
        Debug.Log(copy);
    }
    public void LoadSavedState()
    {
        string jsonFromFile = File.ReadAllText(filename);
        SaveData copy = JsonUtility.FromJson<SaveData>(jsonFromFile);

        float cashToLoad = copy.currentCash;
        List<Agent> agentsToLoad = copy.ownedAgents;
        List<Mission> missionsToLoad = copy.activeMissions;
        GetComponent<MissionController>().LoadDataToController(cashToLoad, agentsToLoad, missionsToLoad);
    }

}
