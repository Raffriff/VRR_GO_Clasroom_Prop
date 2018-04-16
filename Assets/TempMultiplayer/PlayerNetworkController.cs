using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkController : MonoBehaviour{

    public GameObject avatar;
    public Transform playerGlobal;
    public Transform playerHead;

    public PhotonView photonView;

    void Start() {
        if (photonView.isMine) {
            playerGlobal = SceneSetup.main.localVRTransform;//GameObject.Find ("[CameraRig]").transform;
            playerHead = SceneSetup.main.localVRHeadTransform;//playerGlobal.Find ("Camera (eye)").transform;

            transform.SetParent (playerHead);
            transform.localPosition = Vector3.zero;

            avatar.SetActive (false);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext (playerGlobal.position);
            stream.SendNext (playerGlobal.rotation);
            stream.SendNext (playerHead.localPosition);
            stream.SendNext (playerHead.localRotation);
        } else {
            transform.position = (Vector3)stream.ReceiveNext ();
            transform.rotation = (Quaternion)stream.ReceiveNext ();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext ();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext ();
        }
    }
}
