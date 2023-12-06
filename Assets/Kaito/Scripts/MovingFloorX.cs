using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----X�������ő��������������X�N���v�g----
public class MovingFloorX : MonoBehaviour
{
    // �R���|�[�l���g
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SurfaceEffector2D surfaceEffector;

    [SerializeField] float xRange; // X�������̈ړ��͈�
    [SerializeField] float speed; // �ړ����x�H

    // �v�Z�p
    Vector2 defaultPos;
    Vector2 prevPos;

    void Start()
    {
        defaultPos = transform.position;
    }

    void FixedUpdate()
    {
        prevPos = rb.position;

        // X���W�̂݉��ړ��iMathf.PingPong�̐��l�����ixRange�j�ύX�ňړ��������ς��j
        Vector2 pos = new Vector2(defaultPos.x + Mathf.PingPong(Time.time, xRange), defaultPos.y);
        rb.MovePosition(pos);

        // ���x���t�Z����
        Vector2 velocity = (pos - prevPos) / Time.deltaTime * speed;

        // ���x��X������SurfaceEffector2D�ɓK�p
        surfaceEffector.speed = velocity.x;
    }
}
