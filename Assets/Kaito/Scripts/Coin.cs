using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�R�C���̓�����----
public class Coin : MonoBehaviour
{
    [SerializeField] float angle; // ���x����]�����邩
    [SerializeField] ParticleSystem particle;
    
    Vector3 axis = Vector3.up; // ��]��

    void Start()
    {
        particle.Play();
    }

    void Update()
    {
        CoinMove();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            particle.Stop();
            Destroy(gameObject);
        }
    }

    // �R�C���̓���
    void CoinMove()
    {
        // axis���ɁA���bangle�x��]������Quaternion���쐬
        Quaternion rot = Quaternion.AngleAxis(angle, axis);
        // ���݂̎��g�̉�]�̏����擾
        Quaternion q = transform.rotation;
        // �������A���g�ɐݒ�iq * rot �� �a�j
        transform.rotation = q * rot;

        // �q�I�u�W�F�N�g�͉�]���Ȃ��悤�ɏ㏑��
        // �����ɂ́A�p�[�e�B�N����Rotation����
        particle.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
