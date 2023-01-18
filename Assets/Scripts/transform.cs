using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp= GameObject.Find("Canvas").GetComponent<MainMenu>().hp;
        GameObject.Find("Canvas").SetActive(false);
    }

    // Update is called once per frame

}
