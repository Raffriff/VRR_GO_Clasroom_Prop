using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour {

    #region Public Variables
    public static SceneSetup main;

    [Header ("References")]
    public GameObject[] localTraineeActivate;
    public GameObject[] localTraineeDeactivate, localCompanionActivate, localCompaniondeactivate;
    #endregion

    #region Mono Methods
    private void Awake() {
        main = this;
        StartSettingUp ();
    }
    #endregion

    #region Public Methods
    public void StartSettingUp() {
        if (MultiplayerManager.main.playerType == MultiplayerManager.LocalPlayerType.Trainee) {
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
            foreach (GameObject toDeactivate in localCompaniondeactivate) {
                toDeactivate.SetActive (false);
            }
        }
    }
    #endregion

    

}
