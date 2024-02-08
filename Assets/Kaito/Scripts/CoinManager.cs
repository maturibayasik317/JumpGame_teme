using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----�Q�[���V�[���̃R�C���Ǘ�----
public class CoinManager : MonoBehaviour
{
    [SerializeField] GameObject[] stageCoins; // �X�e�[�W��ɔz�u����R�C��
    [SerializeField] Sprite coinSprite; // �R�C���摜
    [SerializeField] Image[] coinImages; // UI�Ƃ��ĕ\������R�C��
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject uiCanvas;

    // �l�������R�C������\������e�L�X�g
    [SerializeField] Text coinNumText;

    Coin coinScript;
    MainGameUI mainUIScript;

    // �ǂ̃V�[���ɂ��邩
    public enum GameSceneType
    {
        STAGE_SELECT = 0,
        STAGE_1,
        STAGE_2,
        STAGE_3
    }
    public GameSceneType gameSceneType;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (uiCanvas != null) // �Q�ƃG���[���
        {
            mainUIScript = uiCanvas.GetComponent<MainGameUI>();
        }
        coinScript = coinPrefab.GetComponent<Coin>();
    }

    void Update()
    {
        if (coinNumText != null) // �Q�ƃG���[���
        {
            // �X�e�[�W���ƂɃR�C���������擾
            switch (gameSceneType)
            {
                // �X�e�[�W1
                case GameSceneType.STAGE_1:
                    // �l�������R�C���� / �X�e�[�W�ɔz�u�����R�C����
                    coinNumText.text = $"�R�C�� : {coinScript.PlayerCoin_Stage1} / {stageCoins.Length}";
                    break;
                // �X�e�[�W2
                case GameSceneType.STAGE_2:
                    coinNumText.text = $"�R�C�� : {coinScript.PlayerCoin_Stage2} / {stageCoins.Length}";
                    break;
                // �X�e�[�W3
                case GameSceneType.STAGE_3:
                    coinNumText.text = $"�R�C�� : {coinScript.PlayerCoin_Stage3} / {stageCoins.Length}";
                    break;
            }
        }
        
        ChangeSprite();
    }

    // �R�C���摜�ɕύX����
    void ChangeSprite()
    {
        // �X�e�[�W�I����ʈȊO��
        if (gameSceneType != GameSceneType.STAGE_SELECT)
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
    }

    //--------�X�e�[�W���Ƃ̏���--------

    // �X�e�[�W�Z���N�g�ł̏���
    //void StageSelectCoin()
    //{
        
    //}

    //// �X�e�[�W1�ł̏���
    //void Stage_1Coin()
    //{
        
    //}

    //// �X�e�[�W2�ł̏���
    //void Stage_2Coin()
    //{
        
    //}

    //// �X�e�[�W3�ł̏���
    //void Stage_3Coin()
    //{
        
    //}

    //-------------------------------
}
