using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D Feet;
    private ParticleSystem playerPS;
    private GameObject dashObj;
    public GameObject body;
    public GameObject enemy;
    public AudioSource JumpMusic;
    public float playerSpeed = 4f;
    public float jumpforce;
    public float dashSpeed;
    public float beHurtTime;
    public float dashTime;
    public int collectionsget = 0;
    private int canSuspend = 0;//悬浮判断
    private float hideTimer = 0f;//计时器
    private float suspendTime = 0.5f;//悬浮时间
    private float startDashTimer;//计时
    public float defendTime = 1f;//无敌时间
    public float useDefendTime = 0f;
    private bool isHurt = false;//判断是否受伤，默认是false
    private bool isGround = true;//判断是否处于地面
    public bool isDefend = false;//判断是否无敌
    private float jumpPreinput = 0f;
    private bool isDashing = false;//判断是否处于冲刺状态


    private float facedirection;
    void Start()
    {
        dashObj = transform.GetChild(1).gameObject;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Feet = GetComponent<BoxCollider2D>();
        playerPS = GameObject.FindGameObjectWithTag("Player").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDefend)
        {
            Movement();
            Dash();
        }
            Suspend();
        Defend();
        falljudge();
        hurtjudge();
    }


    private void FixedUpdate()//固定刷新率。0.02s刷新一次,无敌时间，冷却时间，预输入部分
    {
        if (jumpPreinput > 0.08f) { jumpPreinput -= 0.02f; }
        beHurtTime += 0.02f;
        if (useDefendTime > 0f)
        {
            useDefendTime -= 0.02f;
        }
       if (isDefend)
        {
            defendTime -= 0.02f;
            if (defendTime <= 0)
            {
                defendTime = 1f;
                isDefend = false;
                transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }
    void Movement()
    {
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        float horizontalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");

        //控制朝向
        if (anim.GetBool("isattacking") == false)
        {
            if (facedirection != 0 && facedirection * this.transform.localScale.x < 0)//
            {
                transform.localScale = new Vector3(facedirection * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, 1);
                if (isGround)
                {
                    PPS();
                }
            }
        }else//攻击时锁定朝向
        {
            transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, 1);
        }
        //控制移动
        if (horizontalmove != 0)//控制移动
        {
            anim.SetFloat("running", Mathf.Abs(horizontalmove));
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
            PPS();
            anim.SetBool("jumping", true);
            JumpMusic.Play();
        }
    }
    void falljudge()
    {
        anim.SetBool("idel", false);
        if (anim.GetBool("jumping") && rb.velocity.y <= 0)
        {
            rb.gravityScale = 1.44f;
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
        else if (isGround&&anim.GetBool("falling")==true )
        {
            anim.SetBool("falling", false);
            anim.SetBool("onGround", true);
            PPS();
        }
        else   if (anim.GetBool("onGround"))
        {
           AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 0.2f)
            {
                anim.SetBool("onGround", false);
                canSuspend = 0;
            }
        }
    }//下落判定及动画切换

    void hurtjudge()
    {
        if (isHurt)
        {
            anim.SetFloat("running", 0);
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 0.51f)
            {
                anim.SetBool("hurt", false);
                isHurt = false;
          //          Invoke("HurtTime", beHurtTime);
            }
        }

    }
    void PPS()//灰尘粒子
    {
        playerPS.Play();
    }
    void Dash()
    {
        if (!isDashing)
        {
            if (Input.GetButtonDown("dash"))
            {
                dashObj.SetActive(true);
                isDashing = true;
                startDashTimer = dashTime;
            }
        }
        else
        {
            startDashTimer -= Time.deltaTime;
            if (startDashTimer <= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x / dashSpeed, rb.velocity.y);
                isDashing = false;
                dashObj.SetActive(false);
            }
            else
            {
                rb.velocity = new Vector2(this.transform.localScale.x * dashSpeed * playerSpeed, rb.velocity.y);
            }
        }
    }

    void Defend()
    {
        if (Input.GetMouseButtonDown(1) && useDefendTime <= 0)
        {
            isDefend = true;
            transform.GetChild(4).gameObject.SetActive(true);
            useDefendTime = 10f;
        }
    }
    void Suspend() //悬浮
    {
        if (!isGround && Input.GetButtonDown("Suspend")&&canSuspend<=1)
        {
            canSuspend +=1;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;//冻结
            hideTimer = Time.time + suspendTime;//经过悬浮时间后

        }
        if (Time.time >= hideTimer)
        {
            rb.constraints = RigidbodyConstraints2D.None;//解冻
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//碰撞触发器
    {
       if (collision.gameObject.CompareTag("enemies")|| collision.gameObject.CompareTag("crycry"))
        {
            if (!isDashing && !isDefend&& beHurtTime >= 1&&anim.GetBool("isattacking") == false)
            {
                playerHealth x = GameObject.FindGameObjectWithTag("player").GetComponent<playerHealth>();
                Enemy y = collision.GetComponent<Enemy>();
                isHurt = true;
                anim.SetBool("hurt", true);
                beHurtTime = 0f;
                x.DamagePlayer(y.damage);
            }
            
        }

    }
}
