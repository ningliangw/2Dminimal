using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transform : MonoBehaviour
{
    public int hp;
    public bool havetaken = false;
    // Start is called before the first frame update
    void Start()
    {
        hp= GameObject.Find("Canvas").GetComponent<MainMenu>().hp;
        Destroy(GameObject.Find("Canvas"));
      //  GameObject.Find("Canvas").SetActive(false);
    }

    public void Transform2()
    {
        SceneManager.LoadScene(2);
    }
    // Update is called once per frame
     void Update()
    {
       int  i = GameObject.FindGameObjectsWithTag("Finish").Length;
        if (i >1 && havetaken == false)
        {
            if (i > 1)
            {
                Destroy(GameObject.Find("54321"));
            }
            hp= GameObject.Find("Canvas").GetComponent<MainMenu>().hp;
            SceneManager.LoadScene(2);
            havetaken = true;
        }
    }
}
