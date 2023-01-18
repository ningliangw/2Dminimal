using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    public static Bgm instance;//����
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip bgm1, bgm2, bgm3, bgm4;

    private void Awake()
    {
        instance = this;
    }
    public void Bgm1()
    {
        audioSource.clip = bgm1;
        audioSource.Play();
    }
    public void Bgm2()
    {
        audioSource.clip = bgm2;
        audioSource.Play();
    }
    public void Bgm3()
    {
        audioSource.clip = bgm3;
        audioSource.Play();
    }
    public void Bgm4()
    {
        audioSource.clip = bgm4;
        audioSource.Play();
    }
}
