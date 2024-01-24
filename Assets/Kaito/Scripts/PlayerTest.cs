using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�M�~�b�N�̃e�X�g�p----
public class PlayerTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    float xSpeed;
    float ySpeed;
    bool dash = false;

    MovingFloor moveObj = null; // �������̃X�N���v�g

    // �ǂݎ���p�v���p�e�B
    public bool GetDash => dash;

    void Start()
    {
        
    }

    void Update()
    {
        //transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;

        

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            dash = true;
        }
        else
        {
            dash = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector3 addVelocity = Vector2.zero; // ���Z����ړ����x
        
        // ���������Q�Ƃ���Ă���Ƃ�
        if (moveObj != null)
        {
            // x������y���������ꂼ��擾
            addVelocity = new Vector3(moveObj.GetVelocity.x, moveObj.GetVelocity.y);
        }
        transform.position += new Vector3(xSpeed, ySpeed) + addVelocity;
    }

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �������ɏ���Ă���Ƃ�
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            moveObj = collision.gameObject.GetComponent<MovingFloor>();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // �������ɏ���Ă��Ȃ��Ƃ�
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            moveObj = null;
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
