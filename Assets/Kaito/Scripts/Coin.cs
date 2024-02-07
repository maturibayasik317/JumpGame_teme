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
    static int getCoin = 0; // �V�[����ǂݍ��񂾂Ƃ��̂ݏ����������
    public int GetPlayerCoin => getCoin;

    // �X�e�[�W�Z���N�g��ʂŕ\�������R�C���擾��
    static int stageCoinNum;
    public int GetStageCoinNum => stageCoinNum;

    // �ǂ̃V�[���̃R�C����
    enum GameSceneType
    {
        STAGE_SELECT,
        STAGE_1,
        STAGE_2,
        STAGE_3
    }
    [SerializeField] GameSceneType gameSceneType;

    // �R�C���擾���̌��ʉ�
    [SerializeField] AudioClip sound;
    AudioSource audioSource;

    CoinManager coinManagerScript;
    //[SerializeField] GameObject coinResetButton;
    //static SelectSceneButton selectSceneButtonScript;

    void Start()
    {
        particle.Play();
        audioSource = GetComponent<AudioSource>();
        coinManagerScript = GameObject.FindWithTag("UI").GetComponent<CoinManager>();
        //if (coinResetButton)
        //{
        //    selectSceneButtonScript = GameObject.Find("CoinResetButton").GetComponent<SelectSceneButton>();
        //}
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

    void CoinState()
    {
        // �X�e�[�W���ƂɃR�C�������擾
        switch (gameSceneType)
        {
            case GameSceneType.STAGE_SELECT:
                // ����������
                //StageSelectCoin();
                break;
            case GameSceneType.STAGE_1:
                //Stage_1Coin();
                break;
            case GameSceneType.STAGE_2:
                //Stage_2Coin();
                break;
            case GameSceneType.STAGE_3:
                //Stage_3Coin();
                break;
        }
    }

    // �R�C���擾���̏���
    IEnumerator CoinDestroy()
    {
        audioSource.PlayOneShot(sound); // sound��1��炷
        particle.Stop();

        yield return new WaitForSeconds(1.0f);

        getCoin += 1; // UI�Ŏg�p
        Destroy(gameObject);
    }

//-------static�ϐ��̏������p�֐�-------
    static Coin()
    {
        //if (selectSceneButtonScript.GetCoinReset)
        //{
        //    // �V�[�����ǂݍ��܂ꂽ�ۂɁA�ǉ��������\�b�h�iinit�j���Ăяo�����
        //    // Awake��Start�̊ԂŎ��s�����
        //    SceneManager.sceneLoaded += Init;
        //}
        SceneManager.sceneLoaded += Init;
    }
    
    // �V�[�����ǂݍ��܂ꂽ�Ƃ��A�R�C���̎擾����������
    static void Init(Scene loadingScene, LoadSceneMode loadSceneMode)
    {
        getCoin = 0;
    }
//---------------------------------------
}
