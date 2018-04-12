using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPhotonNetworkConnect : Photon.PunBehaviour {

    public string room = "Room";

    private void Start() {
        Debug.Log ("Connecting");
        PhotonNetwork.ConnectUsingSettings ("1");
    }

    public override  void OnJoinedLobby() {
        
    }

    public override void OnJoinedRoom() {
        Debug.Log ("Creating Network Player");
        PhotonNetwork.Instantiate ("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
    }

    public override void OnConnectedToMaster() {
        Debug.Log ("Joined Room");

        RoomOptions roomOptions = new RoomOptions ();
        PhotonNetwork.JoinOrCreateRoom (room, roomOptions, TypedLobby.Default);
    }

}
