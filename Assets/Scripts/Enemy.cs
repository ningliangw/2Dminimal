using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected Animator anim;
    public int health;
    public int damage;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void TakeDamage(int damage)
    {
        health -= damage;
    }
}
