using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�^���������ł���I�u�W�F�N�g�̃X�N���v�g----
public class MovingStraight : MonoBehaviour
{
    [SerializeField] float speed; // �ړ����x
    [SerializeField] float rotationSpeed; // ��]���x

    SpriteRenderer sr = null;
    bool isView = false;

    void Start()
    {
        sr = GetComponent <SpriteRenderer>();
    }

    void Update()
    {
        if (sr.isVisible) // ��ʓ��Ȃ�
        {
            isView = true;
            if (isView)
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            isView = false;
        }
    }
}
