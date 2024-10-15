using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterFootSoundScript : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip audioClip;

    public float basePitch = 1f;

    public float randomPitchOffset = 0.1f;

    public void PlaySound()
    {
        //Debug.Log("playSound");
        audioSource.pitch = basePitch + Random.Range(-randomPitchOffset, randomPitchOffset);
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
