using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�������i�C�����j----
public class MovingFloor : MonoBehaviour
{
    [SerializeField] SurfaceEffector2D surfaceEffector;
    [SerializeField] SpriteRenderer sr;

    // ��������ݒ�i�C���X�y�N�^�[��Ń`�F�b�N������j
    [SerializeField] bool xAxis;
    [SerializeField] bool yAxis;

    [SerializeField] float moveTime; // ������ւ̈ړ�����
    [SerializeField] float speed; // �ړ����x
    [SerializeField] float otherSpeed;

    Vector2 prevPos;
    Vector2 playerPos;
    float elapsedTime = 0; // ���Ԍv���p

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
            prevPos = transform.position;

            if (elapsedTime >= moveTime)
            {
                // �t�����Ɉړ�
                speed *= -1;
                elapsedTime = 0;
            }
            Vector2 otherVelocity = (velocity - prevPos);
            // ���x��X������SurfaceEffector2D�ɓK�p�i��ɏ���Ă���I�u�W�F�N�g�̃X�s�[�h�Ƀv���X�����j
            surfaceEffector.speed = otherVelocity.x;
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
            // y���œ����ۂ̓I�u�W�F�N�g�͉��ړ������Ȃ�
            surfaceEffector.speed = 0;
        }
    }
}
