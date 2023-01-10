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
    private ParticleSystem playerPS;
    public GameObject dashObj;
    public float playerSpeed = 4f;
    public float jumpforce;
    public float dashSpeed;
    public float dashTime;
    private float hideTimer = 0f;//��ʱ��
    private float suspendTime = 0.5f;//����ʱ��
    private float startDashTimer;//��ʱ
    private bool isHurt = false;//�ж��Ƿ����ˣ�Ĭ����false
    private bool isGround = true;//�ж��Ƿ��ڵ���
    private float jumpPreinput = 0f;
    public bool isDashing = false;//�ж��Ƿ��ڳ��״̬

    private float facedirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Feet = GetComponent<BoxCollider2D>();
        playerPS = GameObject.FindGameObjectWithTag("Player").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Dash();
        Suspend();
    }

    private void FixedUpdate()//�̶�ˢ���ʡ�0.02sˢ��һ��,�޵�ʱ�䣬��ȴʱ�䣬Ԥ���벿��
    {
        if (jumpPreinput > 0.08f) { jumpPreinput -= 0.02f; }
    }
    void Movement()
    {
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        float horizontalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");

        //���Ƴ���
        if (facedirection != 0 && facedirection * this.transform.localScale.x < 0)//
        {
            transform.localScale = new Vector3(facedirection * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, 1);
            PPS();
        }
        //�����ƶ�
        if (horizontalmove != 0)//�����ƶ�
        {
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
                isDashing = false;
                dashObj.SetActive(false);
            }
            else
            {
                rb.velocity = new Vector2(this.transform.localScale.x * dashSpeed * playerSpeed, rb.velocity.y) ;
            }
        }
    } 

    void Suspend() //����
    {
        if (!isGround && Input.GetButtonDown("Suspend"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;//����
            hideTimer = Time.time + suspendTime;//��������ʱ���

        }
        if (Time.time >= hideTimer)
        {
            rb.constraints = RigidbodyConstraints2D.None;//�ⶳ
        }
    }
     
}
