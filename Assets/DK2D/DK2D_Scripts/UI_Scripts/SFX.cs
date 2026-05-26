using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    void Start()
    {
        // gets reference to sfx player
        AudioSource sfxPlayer = GetComponent<AudioSource>();

        // adjusts volume to sfx volume in the audio controller
        sfxPlayer.volume = GameManager.Instance.audioController.sfxVolume;
    }
}
