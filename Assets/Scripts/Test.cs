using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public SimpleAudioEvent simpleAudioEvent;
    void OnPlay() {
        Debug.Log("Play");
        
    }

    void OnUse() {
        Debug.Log("Use");
        var audioSource = GetComponent<AudioSource>();
        simpleAudioEvent.Play(audioSource);
    }
}
