﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempNetworkedPlayer : Photon.PunBehaviour {

    public GameObject avatar;
    public Transform playerGlobal;
    public Transform playerHead;

    void Start() {
        Debug.Log ("Hello");

        if (photonView.isMine) {
            playerGlobal = GameObject.Find ("[CameraRig]").transform;
            playerHead = playerGlobal.Find ("Camera (eye)").transform;

            transform.SetParent (playerHead);
            transform.localPosition = Vector3.zero;

            //avatar.SetActive (false);
#if UNITY_EDITOR
            playerHead.gameObject.SetActive (true);
#else
        playerHead.gameObject.SetActive (false);
#endif


        }
    }

    void OnPhotonSerilizeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext (playerGlobal.position);
            stream.SendNext (playerGlobal.rotation);
            stream.SendNext (playerHead.position);
            stream.SendNext (playerHead.rotation);
        } else {
            transform.position = (Vector3)stream.ReceiveNext ();
            transform.rotation = (Quaternion)stream.ReceiveNext ();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext ();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext ();
        }
    }

}
