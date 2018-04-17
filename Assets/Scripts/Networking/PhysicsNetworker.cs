using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsNetworker : MonoBehaviour {

    #region Public Variables
    public List<Rigidbody> rigidbodies;

    public PhotonView photonView;
    #endregion

    #region Public Methods
    public void AddRigidbody(Rigidbody newRigidbody) {
        rigidbodies.Add (newRigidbody);
    }
    #endregion

    #region Network Methods
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

    }
    #endregion

}
