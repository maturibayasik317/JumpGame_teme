using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----UI�n�̃X�N���v�g----
public class UI : MonoBehaviour
{
//----------�R�C���֘A----------
    [SerializeField] GameObject[] stageCoins; // �X�e�[�W��ɔz�u����R�C��
    
    [SerializeField] Sprite coinSprite; // �R�C���摜
    [SerializeField] Image[] coinImages; // UI�Ƃ��ĕ\������R�C��
    [SerializeField] GameObject coinPrefabs;

    // �l�������R�C������\������e�L�X�g
    [SerializeField] Text coinNumText;

    Coin coinScript;
//----------�R�C���֘A----------

//----------���g���C�E�Q�[���N���A�i�I�[�o�[�j----------
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject gameClearImage;
    [SerializeField] GameObject gameOverImage;

    [SerializeField] GameObject player;
    Jump_Dash playerScript;
//----------���g���C�E�Q�[���N���A�i�I�[�o�[�j----------

    void Start()
    {
        coinScript = coinPrefabs.GetComponent<Coin>();
        playerScript = player.GetComponent<Jump_Dash>();
        
        gameClearImage.SetActive(false);
        gameOverImage.SetActive(false);
        retryButton.SetActive(false);
    }

    void Update()
    {
        // �l�������R�C���� / �X�e�[�W�ɔz�u�����R�C����
        coinNumText.text = $"Coin�F{coinScript.GetPlayerCoin}�^{stageCoins.Length}";
        ChangeSprite();
        
        TextAndButton();
    }

    // �R�C���摜�ɕύX����
    void ChangeSprite()
    {
        if (!stageCoins[0])
        {
            coinImages[0].sprite = coinSprite;
        }
        if (!stageCoins[1])
        {
            coinImages[1].sprite = coinSprite;
        }
        if (!stageCoins[2])
        {
            coinImages[2].sprite = coinSprite;
        }
    }

    public void Retry()
    {
        // ���݊J���Ă���V�[�����ēǂݍ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �e�L�X�g�A�{�^���֘A�̕\���E��\��
    void TextAndButton()
    {
        if (!player)
        {
            retryButton.SetActive(true);
            gameOverImage.SetActive(true);
        }
        if (playerScript.GetIsClear)
        {
            retryButton.SetActive(true);
            gameClearImage.SetActive(true);
        }
    }
}
