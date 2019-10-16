using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip[] audioClips;
    AudioSource audioSource;
    public static AudioManager i;

    public void Init () {
        audioSource = GetComponent<AudioSource> ();
        i = this;
    }

    public void PlayOneShot (int index) {
        AudioClip clip;
        try {
            clip = audioClips[index];
        } catch (System.Exception) {

            throw;
        }

        if (!clip) { return; }
        audioSource.PlayOneShot (clip);
    }

}