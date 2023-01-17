using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prop : MonoBehaviour
{
    public GameObject diary1;
    public GameObject diary1Button;
    public GameObject diary2;
    public GameObject diary2Button;
    public GameObject diary3;
    public GameObject diary3Button;
    public GameObject diary4;
    public GameObject diary4Button;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Destroy(gameObject);
            if (diary1.CompareTag("diary1")) diary1Button.SetActive(true);
            if (diary1.CompareTag("diary2")) diary2Button.SetActive(true);
            if (diary1.CompareTag("diary3")) diary3Button.SetActive(true);
            if (diary1.CompareTag("diary4")) diary4Button.SetActive(true);
        }
    }


}
