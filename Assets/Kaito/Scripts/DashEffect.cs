using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�_�b�V�����̃G�t�F�N�g----
public class DashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    // �_�b�V���G�t�F�N�g�̎�������
    [SerializeField] float dashDuration;

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
            // �_�b�V���G�t�F�N�g����
            particle.Play();
            yield return new WaitForSeconds(dashDuration);
            // dashDuration�b�キ�炢�ɃG�t�F�N�g���X�g�b�v
            particle.Stop();
        }
    }
}
