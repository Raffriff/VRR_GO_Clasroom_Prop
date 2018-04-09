using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerManager : Photon.PunBehaviour{

    #region Public Variables
    //Static
    public static MultiplayerManager main;

    [Header("Connection")]
    public string roomName = "test";

    [Header ("Stats")]
    public LocalPlayerType playerType;

    [Header("UI References")]
    public Text debugText;
    public InputField loginField;
    public GameObject loginObject;
    public Toggle isCompanion;
    public Button startButton, logoutButton;
    #endregion

    #region Hidden Variables
    [HideInInspector]
    public PhotonPlayer otherPlayer;
    #endregion

    #region Private Variables
    private bool connectedToMaster = false;
    private bool readyToStart = false;
    #endregion

    #region Enums
    public enum LocalPlayerType { Trainee, Companion }
    #endregion

    #region Mono Methods
    private void Awake() {
        if (enabled) {
            main = this;
            DontDestroyOnLoad (gameObject);

            if (loginField != null)
                loginField.Select ();
        }
    }

    private void Start() {
        PhotonNetwork.ConnectUsingSettings ("0.01");
        isCompanion.isOn = false;
        loginField.gameObject.SetActive (true);
        loginObject.gameObject.SetActive (true);
        logoutButton.gameObject.SetActive (false);
        startButton.interactable = false;
        startButton.gameObject.SetActive (false);
        debugText.text = "Connecting To Master...";
        Debug.Log ("Connecting To Master...");
    }
    #endregion

    #region Network Methods
    public override void OnConnectedToMaster() {
        connectedToMaster = true;
        debugText.text += "\nConnected To Master";
        Debug.Log ("Connected To Master");
    }

    public override void OnDisconnectedFromPhoton() {
        connectedToMaster = false;
        debugText.text += "\nLost connection to master...";
        Debug.Log ("Lost connection to master...");
        Logout ();
        PhotonNetwork.ConnectUsingSettings ("0.01");
    }

    public override void OnJoinedRoom() {
        if (!CheckRoomSpace (PhotonNetwork.player.NickName)) {
            debugText.text += "\nLog in Error: Space not available in room.";
        } else {
            connectedToMaster = false;

            debugText.text += "\nLogged in as: " + PhotonNetwork.room.Name;

            if (PhotonNetwork.player.NickName == "Trainer") {
                debugText.text += " (Staff)";
            }

            logoutButton.gameObject.SetActive (true);
            loginField.gameObject.SetActive (false);
            loginObject.gameObject.SetActive (false);
            isCompanion.gameObject.SetActive (false);
        }

        CheckReadyToStart ();
    }

    public override void OnLeftRoom() {
        debugText.text += "\nLogged out";
        Debug.Log ("Logged out");

        connectedToMaster = true;

        logoutButton.gameObject.SetActive (false);
        loginField.gameObject.SetActive (true);
        loginObject.gameObject.SetActive (true);
        isCompanion.gameObject.SetActive (true);

    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {
        base.OnPhotonPlayerConnected (newPlayer);

        debugText.text += "\nOther user logged in";
        Debug.Log ("Other user logged in");

        CheckReadyToStart ();
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {
        base.OnPhotonPlayerDisconnected (otherPlayer);
        CheckReadyToStart ();
    }
    #endregion

    public void Login() {
        playerType = LocalPlayerType.Trainee;
        PhotonNetwork.player.NickName = "Trainee";
        if (isCompanion.isOn) {
            playerType = LocalPlayerType.Companion;
            PhotonNetwork.player.NickName = "Trainer";
        }
        

        PhotonNetwork.JoinOrCreateRoom (loginField.text, new RoomOptions (), new TypedLobby ());
    }

    public void Logout() {
        isCompanion.isOn = false;
        if (playerType == LocalPlayerType.Companion)
            isCompanion.isOn = true;

        PhotonNetwork.LeaveRoom ();
    }

    public void StartTest() {
        if (readyToStart)
            UnityEngine.SceneManagement.SceneManager.LoadScene (1);
    }

    private bool CheckRoomSpace(string nickName) {
        foreach (PhotonPlayer otherPlayer in PhotonNetwork.otherPlayers) {
            if (otherPlayer.NickName == nickName)
                return false;
        }
        return true;
    } 

    private void CheckReadyToStart() {
        bool traineeReady = false;
        bool trainerReady = false;

        if (playerType == LocalPlayerType.Trainee)
            trainerReady = true;
        else
            traineeReady = true;

        foreach (PhotonPlayer otherPlayer in PhotonNetwork.otherPlayers) {
            if (otherPlayer.NickName == "Trainer")
                trainerReady = true;
            else if (otherPlayer.NickName == "Trainee")
                traineeReady = true;
        }

        if ((traineeReady) && (trainerReady)) {
            readyToStart = true;
            startButton.interactable = true;
            startButton.gameObject.SetActive (true);
            debugText.text += "Ready To Start!";
            Debug.Log ("Ready To Start!");
        } else {
            readyToStart = false;
            startButton.interactable = false;
            startButton.gameObject.SetActive (false);
        }

    }

}