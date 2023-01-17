using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger instance;//����
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip playerJump, playerHurt, playerAttack, playerDevour, playerFall, playerDeath, playerResurrect, playerShield;

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

    public void PlayerAttack()
    {
        audioSource.clip = playerAttack;
        audioSource.Play();
    }
    public void PlayerFall()
    {
        audioSource.clip = playerFall;
        audioSource.Play();
    }
    public void PlayerDeath()
    {
        audioSource.clip = playerDeath;
        audioSource.Play();
    }
    public void PlayerResurrect()
    {
        audioSource.clip = playerResurrect;
        audioSource.Play();
    }
    public void PlayerShield()
    {
        audioSource.clip = playerShield;
        audioSource.Play();
    }
    public void PlayerDevour()
    {
        audioSource.clip = playerDevour;
        audioSource.Play();
    }

}
