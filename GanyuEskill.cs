using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanyuEskill : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    private void OnEnable()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
