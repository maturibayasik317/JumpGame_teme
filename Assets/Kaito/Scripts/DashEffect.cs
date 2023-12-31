using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�_�b�V�����̃G�t�F�N�g----
// �p�[�e�B�N���V�X�e���̃C���X�y�N�^�[��́uduration�v����
// �������Ԃ�ݒ�
public class DashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    Jump_Dash playerScript; // player�̃_�b�V����Ԃ��擾�p

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Jump_Dash>();
    }

    void Update()
    {
        StartCoroutine(DashParticle());
    }

    // �p�[�e�B�N���̏���
    IEnumerator DashParticle()
    {
        if (playerScript.GetDash)
        {
            particle.Play();
            
            yield return new WaitForSeconds(particle.main.duration);
            
            // duration�b�キ�炢�ɃG�t�F�N�g���X�g�b�v
            particle.Stop();
        }
    }
}
