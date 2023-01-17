using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger instance;//µ¥Àý
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip playerJump, playerHurt, playerAttack, playerDevour;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerJump()
    {
        audioSource.clip = playerJump;
        audioSource.Play();
    }

    public void PlayerHurt()
    {
        audioSource.clip = playerHurt;
        audioSource.Play();
    }

    public void PlayerDevour()
    {
        audioSource.clip = playerDevour;
        audioSource.Play();
    }

    public void PlayerAttack()
    {
        audioSource.clip = playerAttack;
        audioSource.Play();
    }

}
