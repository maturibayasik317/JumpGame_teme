using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�M�~�b�N�̃e�X�g�p----
public class PlayerTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid2D;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    float xSpeed;
    float ySpeed;

    // �o���邩�ǂ���
    bool dash = false;
    bool jump = false;

    bool isClear = false;

// �������̑��x���Z�p�̕ϐ�

    // rb.velocity�p
    Vector2 jumpVec = Vector2.zero;
    // ���Z����ړ����x
    Vector2 addVelocity = Vector2.zero;

    bool onMoveFloor = false; // �������̏ォ�ǂ���
    bool jumpDuring = false; // �W�����v�����ǂ���

    MovingFloor floorSprict = null; // �������̃X�N���v�g

//

    bool isground = false;

    // �ǂݎ���p�v���p�e�B
    public bool GetDash => dash;
    public bool GetIsClear => isClear;

    void Update()
    {
        if (isground)
        {
            jump = true;
        }
        // �W�����v����
        if (jump && Input.GetKeyDown(KeyCode.Space))
        {
// �W�����v���������ɒǋL�i�ύX�j

            jumpDuring = true;
            // (AddForce�̒��g)��ϐ��ujumpVec�v�Ƃ��ĕۑ�
            jumpVec = transform.up * jumpPower;
            // ������ۑ������ϐ��ɕύX
            if (onMoveFloor)
            {
                rigid2D.AddForce(jumpVec + addVelocity);
            }
            else
            {
                rigid2D.AddForce(jumpVec);
            }
//
            jump = false;
            isground = false;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            dash = true;
        }
        else
        {
            dash = false;
        }
        
// Update()�̍Ō�ɒǋL

        // ���������Q�Ƃ���Ă���Ƃ�
        if (floorSprict != null)
        {
            // x������y���������ꂼ��擾
            addVelocity = new Vector2(floorSprict.GetVelocity.x, floorSprict.GetVelocity.y);
            
            // �������̏�ŃW�����v���͂��Ă��Ȃ��Ƃ�
            if (!jumpDuring)
            {
                rigid2D.velocity = new Vector2(0, 0) + addVelocity;
            }
        }
//
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //// �������ɏ���Ă���Ƃ�
        //if (collision.gameObject.CompareTag("MoveFloor"))
        //{
        //    floorSprict = collision.gameObject.GetComponent<MovingFloor>();
        //}

        //// �W�����v�ł���
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    jump = true;
        //}

// �W�����v���蕔���ɒǋL

        //�W�����v�̃I���I�t�ɂ��Ďg�p
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MoveFloor")
        {
            isground = true;
            dash = false;
            //fall = false;

            // �ǋL
            jumpDuring = false;
            
            // �n�ʂɒ��n�����痎���o�ߎ��Ԃ����Z�b�g
            //fallElapsedTime = 0.0f;
            //�n�ʂɒ��n������_�b�V�����~
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);

            if (collision.gameObject.tag == "MoveFloor")
            {
                Debug.Log("���ɏ����");
                // �������̃X�N���v�g�擾
                floorSprict = collision.gameObject.GetComponent<MovingFloor>();
                onMoveFloor = true;
            }
        }
        //

        if (collision.gameObject.CompareTag("HitBox") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("�Q�[���I�[�o�[");
        }
    }


//
    void OnCollisionExit2D(Collision2D collision)
    {
        // �������ɏ���Ă��Ȃ��Ƃ�
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            Debug.Log("�����痣�ꂽ");
            floorSprict = null;
            onMoveFloor = false;
        }
    }
//

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            Destroy(gameObject);
            Debug.Log("�Q�[���I�[�o�[");
        }
        if (collision.gameObject.CompareTag("ClearLine"))
        {
            isClear = true;
        }
    }
}
