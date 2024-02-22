using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----�Q�[���V�[���ł�UI�n�̃X�N���v�g----
public class MainGameUI : MonoBehaviour
{
//----------���g���C�E�Q�[���N���A�i�I�[�o�[�j��----------
    [SerializeField] GameObject retryButton; // ���g���C�{�^��
    [SerializeField] GameObject gameClearImage;
    [SerializeField] GameObject gameOverImage;
    [SerializeField] GameObject selectButton; // �X�e�[�W�I���{�^��
    [SerializeField] GameObject titleButton; // �^�C�g���{�^��

    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem clearParticle;
    [SerializeField] float effectTime;

    Jump_Dash playerScript; // �{����
    //PlayerTest playerScript; // �e�X�g
//----------���g���C�E�Q�[���N���A�i�I�[�o�[�j----------

    // �{�^���������̌��ʉ��p
    AudioSource retryAudioSource;
    AudioSource selectAudioSource;
    AudioSource titleAudioSource;

    void Start()
    {
        // �{����
        playerScript = player.GetComponent<Jump_Dash>();
        //playerScript = player.GetComponent<PlayerTest>();

        retryAudioSource = retryButton.GetComponent<AudioSource>();
        selectAudioSource = selectButton.GetComponent<AudioSource>();
        titleAudioSource = titleButton.GetComponent<AudioSource>();

        gameClearImage.SetActive(false);
        gameOverImage.SetActive(false);
        retryButton.SetActive(false);
        selectButton.SetActive(false);
        titleButton.SetActive(false);

        Debug.Log(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        TextAndButton();
    }

    // ���g���C�{�^�����������Ƃ�
    public void Retry()
    {
        StartCoroutine(SceneRetry());
    }

    // �Z���N�g�{�^�����������Ƃ�
    public void Select()
    {
        StartCoroutine(SceneChange_Select());
    }

    // �^�C�g���{�^�����������Ƃ�
    public void Title()
    {
        StartCoroutine(SceneChange_Title());
    }

    // �e�L�X�g�A�{�^���֘A�̕\���E��\��
    void TextAndButton()
    {
        if (player == null)
        {
            retryButton.SetActive(true);
            selectButton.SetActive(true);
            gameOverImage.SetActive(true);
            titleButton.SetActive(true);
        }
        if (playerScript.GetIsClear)
        {
            retryButton.SetActive(true);
            selectButton.SetActive(true);
            gameClearImage.SetActive(true);
            titleButton.SetActive(true);

            StartCoroutine(ClearEffect());
        }
    }

    // �N���A���o
    IEnumerator ClearEffect()
    {
        if (clearParticle != null) // �Q�ƃG���[���
        {
            clearParticle.Play();
            float stopTime = clearParticle.main.duration + effectTime;
            yield return new WaitForSeconds(stopTime);

            //�uduration�v + �ueffectTime�v�b��ɃX�g�b�v������
            Destroy(clearParticle);
        }
    }


    // ���g���C
    IEnumerator SceneRetry()
    {
        Debug.Log(retryAudioSource);
        retryAudioSource.PlayOneShot(retryAudioSource.clip);
        yield return new WaitForSeconds(0.5f);
        // �������Ă���
        // ���݊J���Ă���V�[�����ēǂݍ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // �X�e�[�W�I����ʂ�
    IEnumerator SceneChange_Select()
    {
        selectAudioSource.PlayOneShot(selectAudioSource.clip);
        yield return new WaitForSeconds(0.5f);
        // �������Ă���
        SceneManager.LoadScene("Select_Scene");
    }
    // �^�C�g����ʂ�
    IEnumerator SceneChange_Title()
    {
        titleAudioSource.PlayOneShot(titleAudioSource.clip);
        yield return new WaitForSeconds(0.5f);
        // �������Ă���
        SceneManager.LoadScene("Title_Scene");
    }
}
