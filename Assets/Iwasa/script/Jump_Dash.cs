using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump_Dash : MonoBehaviour
{   //�v���C���[�ɃR���|�[�l���g���Ă�������
    private Vector2 velocity;
    private int JumpCount = 0;//�W�����v�I�����̏���
    private bool jump = false;
    private bool isground;
    bool dash = false;
    bool fall = false;
    public bool GetDash => dash;
    public GameObject text;
    bool isClear = false;
    public bool GetIsClear => isClear;

    public AudioSource jumpAudioSource;//�W�����v�̉�
    public AudioSource dashAudioSource;//�_�b�V���̉�

    Rigidbody2D rigid2D;
    [SerializeField] ContactFilter2D filter2D;

    private float dashElapsedTime = 0.0f;//�_�b�V�����n�܂��Ă���̗����܂ł̗P�\
    private float fallElapsedTime = 0.0f; // �����J�n����̌o�ߎ���
    [SerializeField] float Dash_Distance;//�_�b�V������10~20���炢���I�X�X��
    [SerializeField] float Jump_Power;//�W�����v��500���炢���I�X�X��
    [SerializeField] private int Jump_Count = 1;//�W�����v�\��
    [SerializeField] float Fall_Duration = 2.0f; // �����J�n����̎�������
    [SerializeField] float FallSpeed = 0.1f;//�������x
    [SerializeField] float DefaultGravityScale = 10.0f;
    //�������̑��x�����p
    Vector2 jumpVec = Vector2.zero;
    Vector2 addVelocity = Vector2.zero;
    bool onMoveFloor = false;//���������ǂ���
    bool jumpDuring = false;//�W�����v�����ǂ���
    MovingFloor floorScript = null;

    void MoveDash()//�W�����v����Speace���������̃_�b�V������
    {

        dash = true;
        dashElapsedTime = 0.0f;
        fallElapsedTime = 0.0f; // �_�b�V�����n�܂����̂ŗ����o�ߎ��Ԃ����Z�b�g

        rigid2D.velocity = new Vector2(Dash_Distance / 0.5f, 0); // Dash_Distance / 0.5f �̓_�b�V���ɂ����鎞�Ԃ������Ă��܂�
        rigid2D.gravityScale = 0.0f;

        dashAudioSource.Play();
        Debug.Log ("���ړ�");
    }

    //��������
    void Fall()
    {
        rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
        //rigid2D.velocity = new Vector2(0, -FallSpeed);
        rigid2D.gravityScale = DefaultGravityScale;
        Debug.Log("����");
    }

    void MoveNormal()
    {
        if( Input.GetKeyDown(KeyCode.Space))
            dash = true;
    }
    private void Awake()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        rigid2D.gravityScale = DefaultGravityScale;
    }
    private void Update()
    {
        dashElapsedTime += Time.deltaTime; // �t���[�����ƂɌo�ߎ��Ԃ��X�V
        //�X�y�[�X�ŃW�����v
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if(isground == true) jump = true;
            else if (!isground && !dash)
                MoveDash();
        }

        if (dash && dashElapsedTime > 0.5f)
        {
            dash = false;
            fall = true;
        }

        if (jump)
        { 
            jumpDuring = true;
            rigid2D.velocity = Vector2.zero;
            jumpVec =transform.up * Jump_Power;//������ۑ������ϐ��ɕύX

            jumpAudioSource.Play();

            //�W�����v������
            this.rigid2D.AddForce(transform.up * Jump_Power);
            if(onMoveFloor)
            {
                rigid2D.AddForce(jumpVec + addVelocity);
            } 
            else
            {
                rigid2D.AddForce(jumpVec);
            }

            jump = false;
            isground = false;
            Debug.Log("�W�����v");
        }

        if (GetComponent<Rigidbody2D>().IsTouching(filter2D))
        {
            //�n�ʂɂ�����I�������������ł���悤�ɂ���
            JumpCount = 1;
        }
        Jump_Count = Mathf.Max(0, Jump_Count);

        // �W�����v�����_�b�V�����łȂ��ꍇ�ɂ̂ݗ�������
        if (!dash && !isground && !fall)
        {
            fallElapsedTime += Time.deltaTime;
            if (fallElapsedTime > Fall_Duration)
            {
                fall = true;
            }
        }

        if (fall)
        {
            Fall() ; 
            Debug.Log("����");
        }

        //���������Q�Ƃ���Ă��鎞
        if(floorScript != null)
        {
            addVelocity = new Vector2(floorScript.GetVelocity.x, floorScript.GetVelocity.y);
            //�������̏�ŃW�����v���ĂȂ��Ƃ�
            if(!jumpDuring)
            {
                rigid2D.velocity =new Vector2(0,0) + addVelocity;
            }
        }

        EndGame();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ClearLine"))
        {
            isClear = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //HitBox��������Enemy�ɐG���ƃv���C���[�̔j��
        if(other.gameObject.tag == "HitBox" || other.gameObject.tag == "Enemy ")
        {
            //�v���C���[������
            Destroy(this.gameObject);
            //�Q�[���I�[�o�[�e�L�X�g�̕\��
            //Iwasa�̂Ƃ����Prefab�Ƃ��ăQ�[���I�[�o�[�e�L�X�g���p�ӂ����̂�Inspecter�ɓ���Ă�������
            text.SetActive(true);
        }

        //�W�����v�̃I���I�t�ɂ��Ďg�p
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "MoveFloor")
        {
            isground = true;
            dash = false;
            fall = false;
            jumpDuring = false;
            // �n�ʂɒ��n�����痎���o�ߎ��Ԃ����Z�b�g
            fallElapsedTime = 0.0f;
            //�n�ʂɒ��n������_�b�V�����~
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);

            if(other.gameObject.tag == "MoveFloor") 
            {
                floorScript = other.gameObject.GetComponent<MovingFloor>();
                onMoveFloor = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("MoveFloor"))
        {
            Debug.Log("�����痣�ꂽ");
            floorScript = null;
            onMoveFloor = false;
        }
    }

    private void EndGame()
    {
        if(Input.GetKey(KeyCode.Exclaim))
        {
            
        }
    }
}
