using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileCreationController : MonoBehaviour
{
    [SerializeField]
    RawImage[] images;
    RawImage chosenImg;
    [SerializeField]
    InputField nameInputField;
    int chosenImgNum = -100;

    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ImageSelected(int imgNum)
    {
        if (chosenImg != null)
        {
            chosenImg.color = Color.white;
        }

        chosenImg = images[imgNum];
        chosenImgNum = imgNum;
        chosenImg.color = new Color(chosenImg.color.r, chosenImg.color.g, chosenImg.color.b, 0.5f);
    }

    public void DoneButton()
    {
        
        // If they chose a name and profile image
        if (!string.IsNullOrEmpty(nameInputField.text) && chosenImgNum != -100)
        {
            // Store the profile image number the user chose
            PlayerPrefs.SetInt("ProfileImage", chosenImgNum);
            // Store the custom name the user chose
            PlayerPrefs.SetString("ProfileName", nameInputField.text);

            // Load the game
            SceneManager.LoadScene("MainGame");
        }
    }

    public void BackButton()
    {
        // Code to return to previous menu (whether it's a scene or game object)
        SceneManager.LoadScene("MainMenu");
    }
}
