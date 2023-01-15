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
    private int canSuspend = 0;//�����ж�
    private float hideTimer = 0f;//��ʱ��
    private float suspendTime = 0.5f;//����ʱ��
    private float startDashTimer;//��ʱ
    public float defendTime = 1f;//�޵�ʱ��
    public float useDefendTime = 0f;
    private bool isHurt = false;//�ж��Ƿ����ˣ�Ĭ����false
    private bool isGround = true;//�ж��Ƿ��ڵ���
    public bool isDefend = false;//�ж��Ƿ��޵�
    private float jumpPreinput = 0f;
    private bool isDashing = false;//�ж��Ƿ��ڳ��״̬


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


    private void FixedUpdate()//�̶�ˢ���ʡ�0.02sˢ��һ��,�޵�ʱ�䣬��ȴʱ�䣬Ԥ���벿��
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

        //���Ƴ���
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
        }else//����ʱ��������
        {
            transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, 1);
        }
        //�����ƶ�
        if (horizontalmove != 0)//�����ƶ�
        {
            anim.SetFloat("running", Mathf.Abs(horizontalmove));
            rb.velocity = new Vector2(horizontalmove * playerSpeed, rb.velocity.y);
        }
        isGround = Feet.IsTouchingLayers(LayerMask.GetMask("ground"));
        if (Input.GetButtonDown("Jump"))//��Ծ
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
    }//�����ж��������л�

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
    void PPS()//�ҳ�����
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
    void Suspend() //����
    {
        if (!isGround && Input.GetButtonDown("Suspend")&&canSuspend<=1)
        {
            canSuspend +=1;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;//����
            hideTimer = Time.time + suspendTime;//��������ʱ���

        }
        if (Time.time >= hideTimer)
        {
            rb.constraints = RigidbodyConstraints2D.None;//�ⶳ
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//��ײ������
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
