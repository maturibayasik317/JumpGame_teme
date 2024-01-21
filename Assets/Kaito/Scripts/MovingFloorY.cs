using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----Y�������ő��������������X�N���v�g----
public class MovingFloorY : MonoBehaviour
{
    // �R���|�[�l���g
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SurfaceEffector2D surfaceEffector;
    [SerializeField] SpriteRenderer sr;

    //[SerializeField] bool xAxis;
    //[SerializeField] bool yAxis;
    [SerializeField] float yRange; // Y�������̈ړ��͈�
    [SerializeField] float speed; // �ړ����x

    // �v�Z�p
    Vector2 defaultPos;
    Vector2 prevPos;

    void Start()
    {
        defaultPos = transform.position; // �����ʒu�擾
    }

    void FixedUpdate()
    {
        if (sr.isVisible)
        {
            Move();
        }
    }

    void Move()
    {
        prevPos = rb.position; // ���݈ʒu�擾
        float t = Time.time * speed;

        // Y���W�̂݉��ړ� : Mathf.PingPong�́ut�v�ňړ����x�A�uyRange�v�ňړ�����
        Vector2 pos = new Vector2(defaultPos.x, defaultPos.y + Mathf.PingPong(t, yRange));
        rb.MovePosition(pos);

        //// ���x���t�Z����
        //Vector2 velocity = (pos - prevPos) / Time.deltaTime * 50;

        //// ���x��Y������SurfaceEffector2D�ɓK�p�i��ɏ���Ă���I�u�W�F�N�g�̃X�s�[�h�Ƀv���X�����j
        //surfaceEffector.speed = velocity.y;
    }
}
