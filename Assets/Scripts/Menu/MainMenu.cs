using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{
    public GameObject help;
    public GameObject start;
    public GameObject setup;
    public GameObject production;
    public AudioMixer audioMixer;
    public int hp = 99999;
    public int hur=-1;
    public void PlayGame1()
    {
        hp = 4;
            SceneManager.LoadScene(1);
    }
    public void PlayGame2()
    {
            SceneManager.LoadScene(1);
        
    }
    public void Transform2()
    {
        SceneManager.LoadScene(2);
    }
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
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
    public void SetVolumn(float value)
    {
        audioMixer.SetFloat("MainVolumn", value);
    }

}
