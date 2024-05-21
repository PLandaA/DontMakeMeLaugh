using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    AudioSource audioSource;

    void Start () {
        instance = this;
        audioSource = GetComponent<AudioSource>();

    }

    public void setSound (AudioClip ac) {
        audioSource.clip = ac;
        audioSource.Play();

    }

}
