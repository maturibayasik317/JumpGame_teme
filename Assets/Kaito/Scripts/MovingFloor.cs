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
    [SerializeField] float otherSpeed; // ��ɏ���Ă���I�u�W�F�N�g�̃X�s�[�h�Ɋ֌W

    // �v�Z�p
    Vector2 defaultPos;
    Vector2 prevPos;
    float elapsedTime = 0; // ���Ԍv���p

    void Start()
    {
        defaultPos = transform.localPosition; // �����ʒu�擾
    }

    void FixedUpdate()
    {
        if (sr.isVisible) // ��ʓ��Ȃ�
        {
            elapsedTime += Time.deltaTime; // �o�ߎ���
            
            // x�������Ɉړ�
            if (xAxis)
            {
                // ���x
                Vector3 velocity = new Vector3(speed, 0) * Time.deltaTime;
                transform.localPosition += velocity;

                if (elapsedTime >= moveTime)
                {
                    // �t�����Ɉړ�
                    speed *= -1;
                    elapsedTime = 0;
                }
                //Vector2 otherVelocity = (velocity - ���݈ʒu)
                // ���x��X������SurfaceEffector2D�ɓK�p�i��ɏ���Ă���I�u�W�F�N�g�̃X�s�[�h�Ƀv���X�����j
                surfaceEffector.speed = velocity.x;
            }

            // y�������Ɉړ�
            if (yAxis)
            {
                // ���x
                Vector3 velocity = new Vector3(0, speed) * Time.deltaTime;
                transform.localPosition += velocity;

                if (elapsedTime >= moveTime)
                {
                    // �t�����Ɉړ�
                    speed *= -1;
                    elapsedTime = 0;
                }

                //// y���W�̂݉��ړ� : Mathf.PingPong�́ut�v�ňړ����x�A�umoveTime�v�ňړ�����
                //Vector2 pos = new Vector2(defaultPos.x, defaultPos.y + Mathf.PingPong(t, moveTime));
                //Vector2 velocity = (pos - prevPos) / Time.deltaTime * otherSpeed;

                //// ���x��y������SurfaceEffector2D�ɓK�p�i��ɏ���Ă���I�u�W�F�N�g�̃X�s�[�h�Ƀv���X�����j
                //surfaceEffector.speed = velocity.y;
            }
        }
        
    }
}
