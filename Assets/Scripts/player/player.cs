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
    public GameObject tentacle;
    public GameObject hand;
    public GameObject rib;
    public GameObject eyeball;
    public float dashCD;
    private float DashCD = 0;
    public AudioSource JumpMusic;
    public Vector2 respawnPosition;//�����
    public float playerSpeed = 4f;
    public float jumpforce;
    public float dashSpeed;
    public float beHurtTime;
    public float dashTime;
    private int canSuspend = 0;//�����ж�
    private float hideTimer = 0f;//��ʱ��
    private float suspendTime = 0.5f;//����ʱ��
    private float startDashTimer;//��ʱ
    public float defendTime = 1f;//�޵�ʱ��
    public float useDefendTime = 0f;
    private bool isHurt = false;//�ж��Ƿ����ˣ�Ĭ����false
    private bool isGround = true;//�ж��Ƿ��ڵ���
    public bool isDefend = false;//�ж��Ƿ��޵�
    private bool canJump = false;//�ж��ܷ���Ծ
    private bool canDefend = false;//�ж��Ƿ���ʹ�û���
    private bool can_Suspend = false;//�ж��ܷ�����
    private bool canDash = false;//�ж��ܷ���
    private bool isEnable = false;
    private float jumpPreinput = 0f;
    //private PolygonCollider2D poly;
    private bool isDashing = false;//�ж��Ƿ��ڳ��״̬


    private float facedirection;
    void Start()
    {
       // poly = GetComponent<PolygonCollider2D>();
        dashObj = transform.GetChild(1).gameObject;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Feet = GetComponent<BoxCollider2D>();
        playerPS = GameObject.FindGameObjectWithTag("Player").GetComponent<ParticleSystem>();
        respawnPosition = transform.position;//��Ϸ�տ�ʼʱ����ҵ������㣬���ǵ�ǰ�ĳ�ʼλ�õ�
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        if (!isDefend&&!isEnable)
        {
            Movement();
            Dash();
        }
        if (!isEnable)
        {
            Suspend();
            Defend();
        }
        falljudge();
        hurtjudge();
    }


    private void FixedUpdate()//�̶�ˢ���ʡ�0.02sˢ��һ��,�޵�ʱ�䣬��ȴʱ�䣬Ԥ���벿��
    {
        if (jumpPreinput > 0.08f) { jumpPreinput -= 0.02f; }
        if (DashCD > 0f) { DashCD -= 0.02f; }
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
        if (Input.GetButtonDown("Jump")/* && canJump*/)//��Ծ
        {
            jumpPreinput = 0.18f;
        }
        if (jumpPreinput > 0.1f && isGround)
        {
            rb.gravityScale = 1f;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            PPS();
            anim.SetBool("jumping", true);
            JumpMusic.Play();
            SoundMananger.instance.PlayerJump();//��Ч
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1),
                              Mathf.Infinity, LayerMask.GetMask("ground"));
            if (hit.collider.CompareTag("platform"))//��⵽ƽ̨
            {
                transform.GetComponent<PolygonCollider2D>().enabled = false;
                StartCoroutine(EnableCollider());

            }
        }

    }
    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        transform.GetComponent<PolygonCollider2D>().enabled = true;

    }

    void falljudge()
    {
        anim.SetBool("idel", false);
        if (anim.GetBool("jumping") && rb.velocity.y <= 1.2)
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
            SoundMananger.instance.PlayerFall();
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 0.2f)
            {
                anim.SetBool("onGround", false);
                canSuspend = 0;
            }
        }
    }//�����ж��������л�

    void Nothurt()
    {
        anim.SetBool("hurt", false);
    }
    void hurtjudge()
    {
        if (isHurt)
        {
            anim.SetFloat("running", 0);
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 0.51f)
            {
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
        if (rb.velocity.y > jumpforce &&isDashing && canDash)
        {
            rb.velocity = new Vector2(rb.velocity.x,0.2f* jumpforce);
        }
        if (!isDashing)
        {
            if (Input.GetButtonDown("dash") && canDash&&DashCD<=0)
            {
                dashObj.SetActive(true);
                isDashing = true;
                startDashTimer = dashTime;
                DashCD = dashCD;
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
        if (Input.GetMouseButtonDown(1) && useDefendTime <= 0 && canDefend)
        {
            SoundMananger.instance.PlayerShield();
            isDefend = true;
            transform.GetChild(4).gameObject.SetActive(true);
            useDefendTime = 10f;
        }
    }
    void Suspend() //����
    {
        if (!isGround && Input.GetButtonDown("Suspend")&&canSuspend<=1 && can_Suspend)
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
    void RecoveyEnable()
    {
        isEnable = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)//��ײ������
    {
       if (collision.gameObject.CompareTag("enemies")|| collision.gameObject.CompareTag("crycry"))
        {
            if (!isDashing && !isDefend&& beHurtTime >=0.8f&&anim.GetBool("isattacking") == false)
            {
                playerHealth x = GameObject.FindGameObjectWithTag("player").GetComponent<playerHealth>();
                Enemy y = collision.GetComponent<Enemy>();
                if (y.health > 0)
                {
                    isHurt = true;
                    anim.SetBool("hurt", true);
                    if (collision.gameObject.layer == 11&&y.repel!=0)
                    {
                        isEnable = true;
                        rb.velocity = new Vector2(y.repel, rb.velocity.y);
                        Invoke("RecoveyEnable", 2f);
                    }
                    
                    beHurtTime = 0f;
                    x.DamagePlayer(y.damage);
                    if (y.health <= 0)
                    {
                        DashCD = 0;
                    }
                    SoundMananger.instance.PlayerHurt();//��Ч
                }
            }
            
        }
       //��������
       if (collision.gameObject.CompareTag("Tentacle"))
        {
            tentacle.SetActive(true);
            canJump = true;
        }
        if (collision.gameObject.CompareTag("hand"))
        {
            hand.SetActive(true);
            canDefend = true;
        }
        if (collision.gameObject.CompareTag("rib"))
        {
            rib.SetActive(true);
            can_Suspend = true;
        }
        if (collision.gameObject.CompareTag("eyeball"))
        {
            eyeball.SetActive(true);
            canDash = true;
        }

        //�浵��
        if (collision.gameObject.CompareTag("archivePoint"))
        {
            respawnPosition = collision.transform.position;
        }

        //bgm
        if (collision.gameObject.CompareTag("bgm1")) Bgm.instance.Bgm1();
        if (collision.gameObject.CompareTag("bgm2")) Bgm.instance.Bgm2();
        if (collision.gameObject.CompareTag("bgm3")) Bgm.instance.Bgm3();
        if (collision.gameObject.CompareTag("bgm4")) Bgm.instance.Bgm4();
    }
}
