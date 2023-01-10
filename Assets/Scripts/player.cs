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
    private bool isHurt = false;//�ж��Ƿ����ˣ�Ĭ����false
    private bool isGround = true;//�ж��Ƿ��ڵ���
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
            anim.SetBool("jumping", true);
        }
    }
}
