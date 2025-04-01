using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource heartBeat;

    public AudioClip forestSound;
    public AudioClip heartbeatSoundEffect;

    void Start()
    {
        bgSource.clip = forestSound;
        bgSource.Play();
        heartBeat.clip = heartbeatSoundEffect;
        heartBeat.Play();
    }

    public void ChangeHeartBeat(float Speed)
    {   
        heartBeat.pitch = Mathf.Lerp(1f,3f, Speed);
        heartBeat.volume = Mathf.Lerp(0.5f,1f, Speed);
        bgSource.volume = Mathf.Lerp(1f,0.6f, Speed);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
