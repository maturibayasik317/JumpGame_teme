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

    Vector2 oldPos; // ���O�̈ʒu���擾
    Vector2 playerVelocity; // �v���C���[�ɉ��Z���鑬�x
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
            Vector2 velocity = new Vector2(speed, 0) * Time.deltaTime;
            transform.localPosition += (Vector3)velocity;

            if (elapsedTime >= moveTime)
            {
                // �t�����Ɉړ�
                speed *= -1;
                elapsedTime = 0;
            }

            // �i�񂾋������o���A���ԂŊ���
            playerVelocity.x = (velocity.x - oldPos.x) / Time.deltaTime;
            oldPos.x = transform.position.x; // ���O�̏��̈ʒu��ۑ�
        }

        // y�������Ɉړ�
        if (yAxis)
        {
            // ���x
            Vector2 velocity = new Vector2(0, speed) * Time.deltaTime;
            transform.localPosition += (Vector3)velocity;

            if (elapsedTime >= moveTime)
            {
                // �t�����Ɉړ�
                speed *= -1;
                elapsedTime = 0;
            }

            playerVelocity.y = (velocity.y - oldPos.y) / Time.deltaTime;
            oldPos.y = transform.position.y;
        }
    }
}
