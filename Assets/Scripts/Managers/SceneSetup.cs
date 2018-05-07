﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour {

    #region Public Variables
    public static SceneSetup main;

    public Transform trainerTransform;
    public string photonVoiceString;

    [Header ("References")]
    public Camera vrCamera;
    public Transform localVRTransform;
    public Transform localVRHeadTransform;
    public Transform localVRHandRight, localVRHandLeft;
    public List<Material> altAvatarMaterials;
    #endregion

    #region Mono Methods
    private void Awake() {
        main = this;
    }

    private void Update() {
        if ((Input.GetKeyDown (KeyCode.R)) && (Input.GetKey (KeyCode.LeftShift)))
            RestartExperience ();
    }
    #endregion

    #region Public Methods
    public void StartSettingUp() {
        if (vrCamera != null)
            vrCamera.enabled = true;
        if (LobbyManager.main.playerType == LobbyManager.LocalPlayerType.Trainee) {

        } else {
            localVRTransform.position = trainerTransform.position;
            localVRTransform.rotation = trainerTransform.rotation;
        }
        Debug.Log ("Creating Network Player");
        PlayerNetworkController networkPlayer = PhotonNetwork.Instantiate ("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0).GetComponent<PlayerNetworkController> ();
    }

    public void RestartExperience() {
        PhotonNetwork.LeaveRoom ();
        UnityEngine.SceneManagement.SceneManager.LoadScene (0);
    }
    #endregion

    

}
