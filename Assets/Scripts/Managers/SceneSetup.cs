using System.Collections;
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
    #endregion

    #region Mono Methods
    private void Awake() {
        main = this;
    }
    #endregion

    #region Public Methods
    public void StartSettingUp() {
        if (vrCamera != null)
            vrCamera.enabled = true;
        if (LobbyManager.main.playerType == LobbyManager.LocalPlayerType.Trainee) {
            /*
             * foreach (GameObject toActivate in localTraineeActivate) {
                toActivate.SetActive (true);
            }
            foreach (GameObject toDeactivate in localTraineeDeactivate) {
                toDeactivate.SetActive (false);
            }*/
        } else {
            localVRTransform.position = trainerTransform.position;
            localVRTransform.rotation = trainerTransform.rotation;
            /*
             * foreach (GameObject toActivate in localCompanionActivate) {
                toActivate.SetActive (true);
            }
            foreach (GameObject toDeactivate in localCompanionDeactivate) {
                toDeactivate.SetActive (false);
            }
            */
        }
        Debug.Log ("Creating Network Player");
        PhotonNetwork.Instantiate ("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
        PhotonNetwork.Instantiate (photonVoiceString, Vector3.zero, Quaternion.identity, 0);
    }
    #endregion

    

}
