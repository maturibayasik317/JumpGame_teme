using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//----�R�C���̓�����----
public class Coin : MonoBehaviour
{
    [SerializeField] float angle; // ���x����]�����邩
    [SerializeField] ParticleSystem particle;

    Vector3 axis = Vector3.up; // ��]��

    // �v���C���[���擾�����R�C����
    // ���Z�b�g�{�^�����������Ƃ��̂ݏ����������
    static int getCoin_Stage1 = 0;
    static int getCoin_Stage2 = 0;
    static int getCoin_Stage3 = 0;
    static int getCoin_Stage4 = 0;

    // �v���p�e�B
    public int PlayerCoin_Stage1
    {
        get { return getCoin_Stage1; }
        set { getCoin_Stage1 = value; }
    }
    public int PlayerCoin_Stage2
    {
        get { return getCoin_Stage2; }
        set { getCoin_Stage2 = value; }
    }
    public int PlayerCoin_Stage3
    {
        get { return getCoin_Stage3; }
        set { getCoin_Stage3 = value; }
    }
    public int PlayerCoin_Stage4
    {
        get { return getCoin_Stage4; }
        set { getCoin_Stage4 = value; }
    }

    // �R�C���擾���̌��ʉ��p
    AudioSource audioSource;

    CoinManager coinManagerScript;

    void Start()
    {
        particle.Play();
        audioSource = GetComponent<AudioSource>();
        coinManagerScript = GameObject.Find("CoinManager").GetComponent<CoinManager>();
    }

    void Update()
    {
        CoinMove();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CoinDestroy());
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

    // �R�C���擾���̏���
    IEnumerator CoinDestroy()
    {
        audioSource.PlayOneShot(audioSource.clip); // sound��1��炷
        particle.Stop();

        yield return new WaitForSeconds(0.8f);

        // �X�e�[�W1
        if (coinManagerScript.gameSceneType == CoinManager.GameSceneType.STAGE_1)
        {
            getCoin_Stage1 += 1; // UI�Ŏg�p
        }
        // �X�e�[�W2
        else if (coinManagerScript.gameSceneType == CoinManager.GameSceneType.STAGE_2)
        {
            getCoin_Stage2 += 1;
        }
        // �X�e�[�W3
        else if (coinManagerScript.gameSceneType == CoinManager.GameSceneType.STAGE_3)
        {
            getCoin_Stage3 += 1;
        }
        // �X�e�[�W�S
        else if (coinManagerScript.gameSceneType == CoinManager.GameSceneType.STAGE_4)
        {
            getCoin_Stage4 += 1;
        }

        Destroy(gameObject);
    }
}
