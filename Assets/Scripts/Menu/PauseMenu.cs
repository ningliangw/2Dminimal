using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("player").transform.GetChild(2).GetComponent<playerAttack>().anim.SetBool("isattacking", false);
        GameObject.FindGameObjectWithTag("player").transform.GetChild(2).GetComponent<playerAttack>().enabled = false;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        StartCoroutine(Close());
        Time.timeScale = 1f;
    }
    IEnumerator Close()
    {
        yield return new WaitForSeconds(0.2f);//—” ±
        GameObject.FindGameObjectWithTag("player").transform.GetChild(2).GetComponent<playerAttack>().enabled = true;
    }

    public void end()
    {
        pauseMenu.SetActive(false);
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
