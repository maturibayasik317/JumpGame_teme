using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----�Q�[���V�[���ł�UI�n�̃X�N���v�g----
public class MainGameUI : MonoBehaviour
{
//----------���g���C�E�Q�[���N���A�i�I�[�o�[�j��----------
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject gameClearImage;
    [SerializeField] GameObject gameOverImage;
    [SerializeField] GameObject selectButton; // �X�e�[�W�I���{�^��
    [SerializeField] GameObject titleButton; // �^�C�g���{�^��

    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem clearParticle;

    Jump_Dash playerScript; // �{����
    //PlayerTest playerScript; // �e�X�g
//----------���g���C�E�Q�[���N���A�i�I�[�o�[�j----------

    void Start()
    {
        // �{����
        playerScript = player.GetComponent<Jump_Dash>();
        //playerScript = player.GetComponent<PlayerTest>();
        
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
        // ���݊J���Ă���V�[�����ēǂݍ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �Z���N�g�{�^�����������Ƃ�
    public void Select()
    {
        SceneManager.LoadScene("Select_Scene");
    }

    // �^�C�g���{�^�����������Ƃ�
    public void Title()
    {
        SceneManager.LoadScene("Title_Scene");
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
            yield return new WaitForSeconds(clearParticle.main.duration);
            //�uduration�v�b��ɃX�g�b�v
            Destroy(clearParticle);
        }
    }
}
