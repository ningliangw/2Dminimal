using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prop : MonoBehaviour
{
    public GameObject button;
    public GameObject diary1;
    public GameObject diary2;
    public GameObject diary3;
    public GameObject diary4;
    public GameObject tentacle2;
    public GameObject ring;
    public GameObject eyeball2;
    public GameObject spine;
    public GameObject vertebra;

    public void OpenDiary1()
    {
        diary1.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseDiary1()
    {
        diary1.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenDiary2()
    {
        diary2.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseDiary2()
    {
        diary2.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenDiary3()
    {
        diary3.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseDiary3()
    {
        diary3.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenDiary4()
    {
        diary4.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseDiary4()
    {
        diary4.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenTentacle2()
    {
        tentacle2.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseTentacle2()
    {
        tentacle2.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenRing()
    {
        ring.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseRing()
    {
        ring.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenEyeball2()
    {
        eyeball2.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseEyeball2()
    {
        eyeball2.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenSpine()
    {
        spine.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseSpine()
    {
        spine.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenVertebra()
    {
        vertebra.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseVertebra()
    {
        vertebra.SetActive(false);
        Time.timeScale = 1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Destroy(gameObject);
            button.SetActive(true);
        }
    }


}
