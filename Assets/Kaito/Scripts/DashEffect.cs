using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�_�b�V�����̃G�t�F�N�g----
public class DashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    [SerializeField] float duration; // �p�[�e�B�N���̎�������

    Jump_Dash playerScript; // player�̃_�b�V����Ԃ��擾�p

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Jump_Dash>();
    }

    void Update()
    {
        StartCoroutine("DashParticle", duration);
    }

    // �p�[�e�B�N���̏���
    IEnumerator DashParticle(float duration)
    {
        if (playerScript.GetDash)
        {
            particle.Play();
            
            yield return new WaitForSeconds(duration);
            
            // duration�b��ɃG�t�F�N�g���X�g�b�v
            particle.Stop();
        }
    }
}
