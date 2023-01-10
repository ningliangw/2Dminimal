using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D Feet;
    public float playerSpeed = 4f;
    public float jumpforce;
    public float doublejumpforce;
    private bool isHurt = false;//判断是否受伤，默认是false
    private bool isGround = true;//判断是否处于地面
    private float jumpPreinput = 0f;

    private float facedirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Feet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void FixedUpdate()//固定刷新率。0.02s刷新一次,无敌时间，冷却时间，预输入部分
    {
        if (jumpPreinput > 0.08f) { jumpPreinput -= 0.02f; }
    }
    void Movement()
    {
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        float horizontalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");

        //控制朝向
        if (facedirection != 0 && facedirection * this.transform.localScale.x < 0)//
        {
            transform.localScale = new Vector3(facedirection * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, 1);
        }
        //控制移动
        if (horizontalmove != 0)//控制移动
        {
            rb.velocity = new Vector2(horizontalmove * playerSpeed, rb.velocity.y);
        }
        isGround = Feet.IsTouchingLayers(LayerMask.GetMask("ground"));
        if (Input.GetButtonDown("Jump"))//跳跃
        {
            jumpPreinput = 0.18f;
        }
        if (jumpPreinput > 0.1f && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
        }
    }
}
