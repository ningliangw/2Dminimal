using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transform : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp= GameObject.Find("Canvas").GetComponent<MainMenu>().hp;
        Destroy(GameObject.Find("Canvas"));
     //   GameObject.Find("Canvas").SetActive(false);
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public void Transform2()
    {
        SceneManager.LoadScene(2);
    }
    // Update is called once per frame

}
