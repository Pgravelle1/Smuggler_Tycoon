using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{


    public AudioMixer mixer;
    public Slider masterVolume;
    public Slider musicVolume;

    public static bool Ispaused = false;

    public GameObject pause;

    public void Unpause()
    {
        Ispaused = false;
    }
    public void ChangeMaster(float value)
    {
        mixer.SetFloat("MasterV", value);
        PlayerPrefs.SetFloat("MasterVPrefs", value);
    }
    public void ChangeMusic(float value)
    {
        mixer.SetFloat("MusicV", value);
        PlayerPrefs.SetFloat("MusicVPrefs", value);
    }

    // Use this for initialization
    void Start()
    {
        float master = PlayerPrefs.GetFloat("MasterVPrefs", 0);
        float music = PlayerPrefs.GetFloat("MusicVPrefs", 0);
        Ispaused = false;
        masterVolume.value = master;
        musicVolume.value = music;

        mixer.SetFloat("MasterV", master);
        mixer.SetFloat("MusicV", music);

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pause != null)
            {
                Ispaused = !Ispaused;
            }
            if (Ispaused)
            {
                pause.SetActive(true);
            }
            else
            {
                pause.SetActive(false);
            }
            if (pause.activeInHierarchy)
            {
                Time.timeScale = 0;
            }
            else if (GameObject.Find("MissionController").GetComponent<roadBlockQuestions>().showGUI == false)
            {
                Time.timeScale = TimeController.CurrentTimeScale;
            }
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
