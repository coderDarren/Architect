/*using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelectionHandler : MonoBehaviour {


    CameraSwitch cameraHandler;
    ArchitectPowers ap;
    PlayerPowers pp;
    CharacterMovement redPlayer, bluePlayer, greenPlayer, yellowPlayer;
    StandardCamera standardCam;
    RTSCamera rtsCam;
    enum PlayerChoice { None, Architect, Red, Blue, Green, Yellow };
    PlayerChoice playerChoice;

    float timeToJoin = 30;
    float joinTimer;
    bool started = false;
    bool choseAPlayer = false;

    void Awake()
    {
        cameraHandler = GetComponent<CameraSwitch>();
        ap = GetComponent<ArchitectPowers>();
        ap.enabled = false;
        pp = GetComponent<PlayerPowers>();
        pp.enabled = false;
        redPlayer = GameObject.FindGameObjectWithTag("RedPlayer").GetComponent<CharacterMovement>();
        bluePlayer = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<CharacterMovement>();
        greenPlayer = GameObject.FindGameObjectWithTag("GreenPlayer").GetComponent<CharacterMovement>();
        yellowPlayer = GameObject.FindGameObjectWithTag("YellowPlayer").GetComponent<CharacterMovement>();
        rtsCam = GetComponent<RTSCamera>();
        rtsCam.enabled = false;
        standardCam = GetComponent<StandardCamera>();
        standardCam.enabled = false;

        joinTimer = timeToJoin;
    }

    void OnEnable()
    {
        JoinGameTimer.AssignPlayer += AssignPlayer;
    }

    void OnDisable()
    {
        JoinGameTimer.AssignPlayer -= AssignPlayer;
    }

    void AssignPlayer()
    {
		Debug.Log(playerChoice);
        switch(playerChoice)
        {

            case PlayerChoice.Architect:
                ap.enabled = true;
                cameraHandler.SwitchToRTS(transform.position);
                ap.myCamera = GetComponent<Camera>();
                break;
            case PlayerChoice.Red:
                pp.enabled = true;
                cameraHandler.SwitchToStandard(redPlayer.transform);
                redPlayer.chosen = true;
                break;
            case PlayerChoice.Blue:
                pp.enabled = true;
                cameraHandler.SwitchToStandard(bluePlayer.transform);
                bluePlayer.chosen = true;
                break;
            case PlayerChoice.Green:
                pp.enabled = true;
                cameraHandler.SwitchToStandard(greenPlayer.transform);
                greenPlayer.chosen = true;
                break;
            case PlayerChoice.Yellow:
                pp.enabled = true;
                cameraHandler.SwitchToStandard(yellowPlayer.transform);
                yellowPlayer.chosen = true;
                break;
        }
    }

    public void ChooseArchitect()
    {
        if (!choseAPlayer)
        {
            choseAPlayer = true;
            playerChoice = PlayerChoice.Architect;
        }
    }
    
    public void ChooseRed()
    {
        if (!choseAPlayer)
        {
            choseAPlayer = true;
            playerChoice = PlayerChoice.Red;
        }
    }

    public void ChooseBlue()
    {
        if (!choseAPlayer)
        {
            choseAPlayer = true;
            playerChoice = PlayerChoice.Blue;
        }
    }

    public void ChooseGreen()
    {
        if (!choseAPlayer)
        {
            choseAPlayer = true;
            playerChoice = PlayerChoice.Green;
        }
    }


    public void ChooseYellow()
    {
        if (!choseAPlayer)
        {
            choseAPlayer = true;
            playerChoice = PlayerChoice.Yellow;
        }
    }
}*/
