using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour {

    #region Public Variables
    public static SceneSetup main;

    public Transform trainerTransform;

    [Header ("References")]
    public Transform localVRTransform;
    public Transform localVRHeadTransform;
    public GameObject[] localTraineeActivate;
    public GameObject[] localTraineeDeactivate, localCompanionActivate, localCompanionDeactivate;
    #endregion

    #region Mono Methods
    private void Awake() {
        main = this;
    }
    #endregion

    #region Public Methods
    public void StartSettingUp() {
        localVRTransform.gameObject.SetActive (true);
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
    }
    #endregion

    

}
