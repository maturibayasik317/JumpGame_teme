using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�v���C���[�����݂���ԏ�ɐ����Ă��镗�̃G�t�F�N�g----
public class WindEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject player;

    void Start()
    {
        particle.Play();
    }

    void Update()
    {
        if (player == null)
        {
            particle.Stop();
        }
    }
}
