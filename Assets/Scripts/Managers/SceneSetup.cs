using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour {

    #region Public Variables
    public static SceneSetup main;

    [Header ("References")]
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
        if (LobbyManager.main.playerType == LobbyManager.LocalPlayerType.Trainee) {
            foreach (GameObject toActivate in localTraineeActivate) {
                toActivate.SetActive (true);
            }
            foreach (GameObject toDeactivate in localTraineeDeactivate) {
                toDeactivate.SetActive (false);
            }
        } else {
            foreach (GameObject toActivate in localCompanionActivate) {
                toActivate.SetActive (true);
            }
            foreach (GameObject toDeactivate in localCompanionDeactivate) {
                toDeactivate.SetActive (false);
            }
        }
    }
    #endregion

    

}
