using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�M�~�b�N�̃e�X�g�p----
public class PlayerTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    float xSpeed;
    float ySpeed;

    // �o���邩�ǂ���
    bool dash = false;
    bool jump = false;
    bool fall = false;

    // rb.velocity�p
    Vector2 jumpForce;
    Vector2 fallForce;

    bool jumpDuring = false; // �W�����v�����ǂ���
    bool fallDuring = false;

    MovingFloor floorSprict = null; // �������̃X�N���v�g

    // �ǂݎ���p�v���p�e�B
    public bool GetDash => dash;

    void Start()
    {
        
    }

    private void Update()
    {
        if (jump && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
//
            jumpDuring = true;
            // �ۑ�
            jumpForce = Vector2.up * jumpPower;
            rb.AddForce(jumpForce);
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

        Vector2 addVelocity = Vector2.zero; // ���Z����ړ����x
        

        // ���������Q�Ƃ���Ă���Ƃ�
        if (floorSprict != null)
        {
            // x������y���������ꂼ��擾
            addVelocity = new Vector2(floorSprict.GetVelocity.x, floorSprict.GetVelocity.y);
            
            // �W�����v�A������Ԃ����ꂼ��Q��

            if (jumpDuring) // �W�����v���̓W�����v�͂��Q�Ƃ�����
            {
                rb.velocity = new Vector2(0, jumpForce.y) + addVelocity;
            }
/*            else if (fallDuring) // ������
            {
                rb.velocity = new Vector2(0, fallForce.y) + addVelocity;
            }
*/
            else // �ʏ펞
            {
                rb.velocity = new Vector2(0, 0) + addVelocity;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �������ɏ���Ă���Ƃ�
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            floorSprict = collision.gameObject.GetComponent<MovingFloor>();
        }

        // �W�����v�ł���
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MoveFloor"))
        {
           jump = true;
        }
        else
        {
            jump = false;
        }

        if (collision.gameObject.CompareTag("HitBox"))
        {
            Destroy(gameObject);
            Debug.Log("�Q�[���I�[�o�[");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // �������ɏ���Ă��Ȃ��Ƃ�
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            floorSprict = null;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            Destroy(gameObject);
            Debug.Log("�Q�[���I�[�o�[");
        }
    }
}
