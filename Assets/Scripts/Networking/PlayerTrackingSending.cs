using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackingSending :  Photon.PunBehaviour{

    public Transform rightHand;

    private void OnPhotonSerializeView(PhotonStream stream, NetworkMessageInfo info) {
        if (isActiveAndEnabled) {
            if (stream.isWriting) {
                Debug.Log ("Sending");
                stream.SendNext (rightHand.position);
                stream.SendNext (rightHand.rotation);
            } else {
                Debug.Log ("Receiving");
                rightHand.position = (Vector3)stream.ReceiveNext ();
                rightHand.rotation = (Quaternion)stream.ReceiveNext ();
            }
        }
    }

}
