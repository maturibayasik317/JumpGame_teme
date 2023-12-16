using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----Y�������ɉ�������G----
public class Enemy : MonoBehaviour
{
    [SerializeField] float yRange; // �ړ��͈�
    [SerializeField] float speed; // �ړ����x

    // ��̃I�u�W�F�N�g�ŊǗ����Ă��邽�߁A�I�u�W�F�N�g�̒��S�ʒu���f��Ȃ��Ƃ����Ȃ�
    // �ŏ���null�ɂ��Ă����Ȃ��ƁA��ʊO�ł�����ɓ����Ă��܂�
    SpriteRenderer sr = null;
    bool isView = false;
    Vector2 startPos;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (sr.isVisible) // �G���J�����͈͓̔��Ȃ�
        {
            isView = true; // �J�����������
            if (isView)
            {
                float t = Time.time * speed;

                // Y���W�̂݉��ړ�
                float pos = startPos.y + Mathf.PingPong(t, yRange);
                transform.position = new Vector3(startPos.x, pos, transform.position.z);
            }
        }
        else
        {
            isView = false;
        }
    }
}
