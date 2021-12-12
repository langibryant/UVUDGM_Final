using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    AudioSource audioData;

    public static SoundManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void PlayShoot()
    {
        audioData.Play(0);
    }
}