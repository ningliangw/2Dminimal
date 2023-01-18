using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject help;
    public GameObject start;
    public GameObject setup;
    public GameObject production;
    public void PlayGame1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGame2()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Start is called before the first frame update
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenHelp()
    {
        help.SetActive(true);
    }

    public void CloseHelp()
    {
        help.SetActive(false);
    }
    public void OpenStart()
    {
        start.SetActive(true);
    }
    public void CloseStart()
    {
        start.SetActive(false);
    }
    public void OpenSetup()
    {
        setup.SetActive(true);
    }
    public void CloseSetup()
    {
        setup.SetActive(false);
    }
    public void OpenProduction()
    {
        production.SetActive(true);
    }
    public void CloseProduction()
    {
        production.SetActive(false);
    }
    
}
