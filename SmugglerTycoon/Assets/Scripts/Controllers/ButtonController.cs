using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject mainPanel, optionsPanel, loadPanel, creditsPanel, howPanel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonEvent(string name)
    {
        switch (name.ToUpper())
        {
            case "PROFILECREATION":
                mainPanel.SetActive(true);
                optionsPanel.SetActive(false);
                loadPanel.SetActive(false);
                creditsPanel.SetActive(false);
                howPanel.SetActive(false);
                SceneManager.LoadScene("ProfileCreation");
                break;
            case "LOAD":
                mainPanel.SetActive(false);
                optionsPanel.SetActive(false);
                loadPanel.SetActive(true);
                howPanel.SetActive(false);
                creditsPanel.SetActive(false);
                break;
            case "BACKTOMENU":
                mainPanel.SetActive(true);
                optionsPanel.SetActive(false);
                loadPanel.SetActive(false);
                howPanel.SetActive(false);
                creditsPanel.SetActive(false);
                break;
            case "OPTIONS":
                mainPanel.SetActive(false);
                optionsPanel.SetActive(true);
                loadPanel.SetActive(false);
                howPanel.SetActive(false);
                creditsPanel.SetActive(false);
                break;
            case "HOWTOPLAY":
                mainPanel.SetActive(false);
                optionsPanel.SetActive(false);
                loadPanel.SetActive(false);
                howPanel.SetActive(true);
                creditsPanel.SetActive(false);
                break;
            case "CREDITS":
                mainPanel.SetActive(false);
                optionsPanel.SetActive(false);
                loadPanel.SetActive(false);
                howPanel.SetActive(false);
                creditsPanel.SetActive(true);
                break;
            case "QUIT":
                Application.Quit();
                break;
                
                // Added GAME OVER as an option
            case "GAME OVER":
                SceneManager.LoadScene("MainMenu");
                break;
            default:
                break;
        }
    }
}
