using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyMinnow.SALSA;
using Photon;

[RequireComponent (typeof (Salsa3D))]
[RequireComponent (typeof (AudioSource))]
public class MicToSalsa : UnityEngine.MonoBehaviour {

    private Salsa3D salsa;
    public PhotonVoiceSpeaker voice;
    public string audioInputDevice;
    private bool isLinked = false;

    private void Awake()
    {
        salsa = GetComponent<Salsa3D> ();
    }

    private void Start()
    {
        if (voice == null)
        {
            salsa.audioClip = Microphone.Start (audioInputDevice, true, 10, 44100);
            salsa.audioSrc.clip = salsa.audioClip;
            salsa.audioSrc.Play ();
        }
    }
    private void Update()
    {
        if ((voice != null) && (!isLinked))
        {
            if (voice.isLinked)
            {
                isLinked = true;
                Debug.LogError ("!!!!!!!!!!\n");
                Debug.LogError (voice.player.source.clip.name);
                salsa.audioSrc.clip = voice.player.source.clip;
                salsa.audioSrc.Play ();
            }
        }
    }
}
