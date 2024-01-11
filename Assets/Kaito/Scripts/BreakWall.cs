using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�_�b�V���ŉ󂹂�ǂ̃X�N���v�g----
// �p�[�e�B�N���V�X�e���̃C���X�y�N�^�[��́uduration�v����
// �ǂ�����܂ł̎��Ԃ�ݒ�
public class BreakWall : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] Collider2D _collider;
    Jump_Dash playerScript;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Jump_Dash>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �_�b�V����Ԃ̃v���C���[������������
        if (collision.gameObject.CompareTag("Player") && playerScript.GetDash)
        {
            _collider.isTrigger = true; // �ʂ蔲������悤�ɂ���
            StartCoroutine(WallDestroy());
        }
    }

    // �ǂ����鏈��
    IEnumerator WallDestroy()
    {
        particle.Play();

        // �G�t�F�N�g�I����ɕǂ�����
        yield return new WaitForSeconds(particle.main.duration);
        Destroy(gameObject);
    }
}
