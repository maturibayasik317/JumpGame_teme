using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----Y�������ő��������������X�N���v�g----
public class MovingFloorY : MonoBehaviour
{
    // �R���|�[�l���g
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SurfaceEffector2D surfaceEffector;

    [SerializeField] float yRange; // Y�������̈ړ��͈�
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

        // Y���W�̂݉��ړ��iMathf.PingPong�̐��l�����iyRange�j�ύX�ňړ��������ς��j
        Vector2 pos = new Vector2(defaultPos.x, defaultPos.y + Mathf.PingPong(Time.time, yRange));
        rb.MovePosition(pos);

        // ���x���t�Z����
        Vector2 velocity = (pos - prevPos) / Time.deltaTime * speed;

        // ���x��Y������SurfaceEffector2D�ɓK�p
        surfaceEffector.speed = velocity.y;
    }
}
