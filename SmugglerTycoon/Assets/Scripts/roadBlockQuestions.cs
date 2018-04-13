using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class roadBlockQuestions : MonoBehaviour
{
    private static int MENU_WIDTH = 1024;
    private static int MENU_HEIGHT = 768;
    private Rect windowRect = new Rect((Screen.width - MENU_WIDTH) / 2, 200, MENU_WIDTH, MENU_HEIGHT);
    public bool showGUI = false;
    string agentName = "Bob";
    List<QuestionIO> questionAnswerList = new List<QuestionIO>();
    QuestionIO questionOne = new QuestionIO();
    QuestionIO questionTwo = new QuestionIO();
    QuestionIO questionThree = new QuestionIO();
    QuestionIO questionFour = new QuestionIO();
    QuestionIO questionFive = new QuestionIO();
    QuestionIO questionSix = new QuestionIO();
    QuestionIO questionSeven = new QuestionIO();
    QuestionIO questionEight = new QuestionIO();
    QuestionIO questionNine = new QuestionIO();
    QuestionIO questionTen = new QuestionIO();
    QuestionIO questionEleven = new QuestionIO();

    QuestionIO[] questionArray = new QuestionIO[11];
    int questionNumber;
    int rnd;
    List<int> questionAlreadyAsked = new List<int>();

    int trustvalue;

    int missionListReference;

    
#warning alex change - nov, 25.
    internal static bool aRoadBlockIsOn;

    //float startTime = 0;
    //float maxTime = 20;
    //float currentTime;

    public enum QuestionState
    {
        Active,
        Start,
        Response
    }
    QuestionState questionState = new QuestionState();
    // Use this for initialization
    void Start()
    {
        
        questionState = QuestionState.Start;
        questionOne.QuestionForm("What's your reason for crossing the border today?",
                                    "Meeting a friend.", 1,
                                    "Just passing through.", 0,
                                    "I'm travelling and wanted to see the area.", 2,
                                    "I'm smuggling drugs into the country.", -2);
        //"You're on a need-to-know basis, and you don't need to know" ,-1,
        //"I don't need REASONS man! I'm an open spirit", -1,
        //"Pfft, who died and made you king?", -1,
        //"For work", -1,
        //);

        questionTwo.QuestionForm("What are you carrying?",
                                      "A lot of maple syrup.", +1,
                                      "I'm bringing some clothing, I went on a shopping spree.", 1,
                                      //"Bio-hazaradous waste, so be careful checking the back.", 1,
                                      "The insurmountable weight that is my depression.", -1,
                                      "Drugs... you want some?", -3);

        questionThree.QuestionForm("What's your name?",
                                      "Reptillius Burkenshire.", -2,
                                      "Yo face.", -1,
                                      "Steven Avery.", -1,
                                      agentName + "." , 2);

        questionFour.QuestionForm("Why should I let you cross the border?",
                                      "Because I really really really want to cross.", 0,
                                      "Because it's your job.", -1,
                                      "Less paper work for you.", 1,
                                      "Drugs... you want some?", -1);

        questionFive.QuestionForm("What’s that smell?",
                                      "Drugs… So many drugs.", -4,
                                      "The body in the back.", -4,
                                      "Air freshener.", 1,
                                      "Teenage spirit.", 0);

        questionSix.QuestionForm("Are you drunk, sir?",
                                      "Nope.", +1,
                                      "Always.", -1,
                                      "Sir, I am over 300lbs, I can never be drunk!", +2,
                                      "You don't expect me to make this *hic* trip sober, do you?", -1);

        questionSeven.QuestionForm("Can I see some I.D.?",
                                      "Since when did I need a passport?", -1,
                                      "No thank you.", -1,
                                      "Sure, here you go.", 1,
                                      "Well ‘I’ can give you some ‘D’", -1);

        questionEight.QuestionForm("How long will you be visiting?",
                                      "Worst case scenario : 5 to life.", +1,
                                      "Two days.", +1,
                                      "Until I'm not here.", -1,
                                      "Until immigration finds me", -2);

        questionNine.QuestionForm("Where will you be staying?",
                                      "In a ditch.", -1,
                                      "Your Mothers house, she invited me!", -3,
                                      "My Mothers house, she invted me!.", +1,
                                      "Some crappy motel, on company money!", +1);

        questionTen.QuestionForm("What's your occupation?",
                                      "Police officer.", 0,
                                      "Drug mule.", -1,
                                      "Currently, NOTHING!.", +1,
                                      "To drive this vehicle across the border", -1);

        questionEleven.QuestionForm("How much dirt is in a hole that’s 3 feet by 3 feet by 6 feet?",
                                      "None, it’s a hole.", +2,
                                      "54 lbs.", -1,
                                      "20 kilos.", -1,
                                      "Is this relevent?", -1);
        questionArray[0] = questionOne;
        questionArray[1] = questionTwo;
        questionArray[2] = questionThree;
        questionArray[3] = questionFour;
        questionArray[4] = questionFive;
        questionArray[5] = questionSix;
        questionArray[6] = questionSeven;
        questionArray[7] = questionEight;
        questionArray[8] = questionNine;
        questionArray[9] = questionTen;
        questionArray[10] = questionEleven;

        questionNumber = 0;
        trustvalue = 0;

        

    }

    // Update is called once per frame
    void Update()
    {
        //if (questionState == QuestionState.Active)
        //{
        //    currentTime = Time.time;
        //}
    }

    public void StartMiniGame(int missionListPosition, string AgentName)
    {
        agentName = AgentName;
        questionArray[2].responseFour = agentName + ".";
        showGUI = true;
        missionListReference = missionListPosition;
        questionState = QuestionState.Start;
        questionNumber = 0;
        trustvalue = 0;
        rnd = Random.Range(0, 10);
        
        questionAlreadyAsked.Clear();
        questionAlreadyAsked.Add(rnd);
        Time.timeScale = 0;
    }

    public void OnGUI()
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 50;
        if (showGUI && !GameObject.Find("MenuBarCanvas").GetComponent<OptionController>().pause.activeInHierarchy)
        {
            
            windowRect = GUILayout.Window(0, windowRect, DoControlsWindow, "Border Patrol", myButtonStyle);
            windowRect.x = Mathf.Clamp(windowRect.x, 0.0f, Screen.width - windowRect.width);
            windowRect.y = Mathf.Clamp(windowRect.y, 0.0f, Screen.height - windowRect.height);
        }
        else
        {
           
        }
#warning alex change - nov, 25.
        aRoadBlockIsOn = showGUI;
    }

    public void DoControlsWindow(int windowID)
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 30;
        switch (questionState)
        {
            case QuestionState.Active:

                if (questionNumber > 3)
                {
                    questionNumber = 0;
                    questionState = QuestionState.Response;
                }
                if (questionNumber < 4 && questionState == QuestionState.Active)
                {
                    GUILayout.Space(40);

                    GUILayout.Label(questionArray[rnd].question, myButtonStyle);


                    GUILayout.Space(20);

                    GUILayout.BeginVertical("box");
                    if (GUILayout.Button(questionArray[rnd].responseOne, myButtonStyle))
                    {
                        Debug.Log("Test 1");
                        Debug.Log(questionArray[rnd].trustValueOne);
                        trustvalue += questionArray[rnd].trustValueOne;
                        questionNumber++;
                        NextNumber();
                        if (questionNumber > 3)
                        {
                            questionNumber = 0;
                            questionState = QuestionState.Response;
                        }

                    }
                    else if (GUILayout.Button(questionArray[rnd].responseTwo, myButtonStyle))
                    {
                        Debug.Log("Test 2");
                        trustvalue += questionArray[rnd].trustValueTwo;

                        questionNumber++;
                        NextNumber();
                        if (questionNumber > 3)
                        {
                            questionNumber = 0;
                            questionState = QuestionState.Response;
                        }


                    }
                    else if (GUILayout.Button(questionArray[rnd].responseThree, myButtonStyle))
                    {
                        Debug.Log("Test 3");
                        trustvalue += questionArray[rnd].trustValueThree;

                        questionNumber++;
                        NextNumber();
                        if (questionNumber > 3)
                        {
                            questionNumber = 0;
                            questionState = QuestionState.Response;
                        }

                    }
                    else if (GUILayout.Button(questionArray[rnd].responseFour, myButtonStyle))
                    {
                        Debug.Log("Test 4");
                        trustvalue += questionArray[rnd].trustValueFour;

                        questionNumber++;
                        NextNumber();
                        if (questionNumber > 3)
                        {
                            questionNumber = 0;
                            questionState = QuestionState.Response;
                        }

                    }
                    
                }
                //currentTime = GUI.HorizontalSlider(new Rect((Screen.width - MENU_WIDTH) / 2, 200, MENU_WIDTH, MENU_HEIGHT)
                //                                    , currentTime, startTime, maxTime);

                if (questionNumber > 3)
                {
                    questionNumber = 0;
                    questionState = QuestionState.Response;
                }

                GUILayout.EndVertical();
                break;
            case QuestionState.Start:
                if (GUILayout.Button("Start", myButtonStyle))
                {
                    questionState = QuestionState.Active;
                    //startTime = Time.time;
                }


                break;
            case QuestionState.Response:
                GUILayout.Space(20);

                if (trustvalue > 0)
                {
                    GUILayout.Label("Okay then, have a nice day sir", myButtonStyle);
                }
                else
                {
                    GUILayout.Label("Please pull to the check section please! (you failed)", myButtonStyle);
                }
                if (GUILayout.Button("End", myButtonStyle))
                {
                    if (trustvalue > 0)
                    {
                        gameObject.GetComponent<MissionController>().PassedMinigame(true, missionListReference);
                    }
                    else
                    {
                        gameObject.GetComponent<MissionController>().PassedMinigame(false, missionListReference);
                    }
                    Time.timeScale = TimeController.CurrentTimeScale;
                    showGUI = false;
                }


                    break;

            default:
                break;
        }




    }

    void NextNumber()
    {
        rnd = Random.Range(0, 10);
        while (questionAlreadyAsked.Contains(rnd))
        {
            rnd = Random.Range(0, 10);
        }
        
        questionAlreadyAsked.Add(rnd);
    }
}
