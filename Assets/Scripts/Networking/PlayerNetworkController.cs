using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkController : MonoBehaviour{

    public Transform avatarHead, avatarHandRight, avatarHandLeft;

    public Vector3 handRotationOffset;

    [HideInInspector]
    public Transform playerGlobal, playerHead, playerHandRight, playerHandLeft;

    public PhotonView photonView;

    void Start() {
        if (photonView.isMine) {
            playerGlobal = SceneSetup.main.localVRTransform;
            playerHead = SceneSetup.main.localVRHeadTransform;
            playerHandRight = SceneSetup.main.localVRHandRight;
            playerHandLeft = SceneSetup.main.localVRHandLeft;

            transform.SetParent (playerHead);
            transform.localPosition = Vector3.zero;

            avatarHead.gameObject.SetActive (false);
            avatarHandRight.gameObject.SetActive (false);
            avatarHandLeft.gameObject.SetActive (false);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext (playerGlobal.position);
            stream.SendNext (playerGlobal.rotation);
            stream.SendNext (playerHead.localPosition);
            stream.SendNext (playerHead.localRotation);
            stream.SendNext (playerHandRight.position);
            stream.SendNext (playerHandRight.rotation);
            stream.SendNext (playerHandLeft.position);
            stream.SendNext (playerHandLeft.rotation);
        } else {
            transform.position = (Vector3)stream.ReceiveNext ();
            transform.rotation = (Quaternion)stream.ReceiveNext ();
            avatarHead.localPosition = (Vector3)stream.ReceiveNext ();
            avatarHead.localRotation = (Quaternion)stream.ReceiveNext ();
            avatarHandRight.position = (Vector3)stream.ReceiveNext ();
            avatarHandRight.rotation = (Quaternion)stream.ReceiveNext () * Quaternion.Euler (handRotationOffset);
            avatarHandLeft.position = (Vector3)stream.ReceiveNext ();
            avatarHandLeft.rotation = (Quaternion)stream.ReceiveNext () * Quaternion.Euler (handRotationOffset);
        }
    }
}
