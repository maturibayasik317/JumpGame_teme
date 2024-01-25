using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�������i�C�����j----
public class MovingFloor : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    // ��������ݒ�i�C���X�y�N�^�[��Ń`�F�b�N������j
    [SerializeField] bool xAxis;
    [SerializeField] bool yAxis;

    [SerializeField] float moveTime; // ������ւ̈ړ�����
    [SerializeField] float speed; // �ړ����x

    Vector2 oldPos = Vector2.zero; // ���O�̈ʒu���擾
    Vector2 velocity = Vector2.zero;
    Vector2 playerVelocity = Vector2.zero; // �v���C���[�ɉ��Z���鑬�x
    float elapsedTime = 0; // ���Ԍv���p

    // �ǂݎ���p�v���p�e�B
    public Vector2 GetVelocity => playerVelocity;

    void Start()
    {
        oldPos = transform.position;
    }

    void FixedUpdate()
    {
        if (sr.isVisible) // ��ʓ��Ȃ�
        {
            Move();
        }
    }

    // ���𓮂���
    void Move()
    {
        elapsedTime += Time.deltaTime; // �o�ߎ���

        // x�������Ɉړ�
        if (xAxis)
        {
            // ���x
            velocity.x = speed * Time.deltaTime;
            transform.localPosition += (Vector3)velocity;

            if (elapsedTime >= moveTime)
            {
                // �t�����Ɉړ�
                speed *= -1;
                elapsedTime = 0;
            }

            // �i�񂾋������o���A���ԂŊ���
            playerVelocity.x = (transform.localPosition.x - oldPos.x) / Time.deltaTime;
            oldPos.x = transform.localPosition.x; // ���O�̏��̈ʒu��ۑ�
        }

        // y�������Ɉړ�
        if (yAxis)
        {
            // ���x
            velocity.y = speed * Time.deltaTime;
            transform.localPosition += (Vector3)velocity;
            
            if (elapsedTime >= moveTime)
            {
                // �t�����Ɉړ�
                speed *= -1;
                elapsedTime = 0;
            }

            playerVelocity.y = (transform.localPosition.y - oldPos.y) / Time.deltaTime;
            oldPos.y = transform.localPosition.y;
        }
    }
}
