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
    [SerializeField] GameObject coinPrefabs;

    // �l�������R�C������\������e�L�X�g
    [SerializeField] Text coinNumText;

    Coin coinScript;

    [SerializeField] GameSceneType gameSceneType;
    bool onStageSelect = false;
    bool onStage_1 = false;
    bool onStage_2 = false;
    bool onStage_3 = false;

    // �uCoin.cs�v�Ŏg�p
    public bool GetOnStageSelect => onStageSelect;
    public bool GetOnStage_1 => onStage_1;
    public bool GetOnStage_2 => onStage_2;
    public bool GetOnStage_3 => onStage_3;

    // �ǂ̃V�[���ɂ��邩
    enum GameSceneType
    {
        STAGE_SELECT,
        STAGE_1,
        STAGE_2,
        STAGE_3
    }

    // �ǂ̃V�[�����𐮐��œn��
    public int GetGameSceneType => (int)gameSceneType;

    void Start()
    {
        coinScript = coinPrefabs.GetComponent<Coin>();
        
        // �X�e�[�W���ƂɃR�C�������擾
        switch (gameSceneType)
        {
            case GameSceneType.STAGE_SELECT:
                // ����������
                StageSelectCoin();
                break;
            case GameSceneType.STAGE_1:
                Stage_1Coin();
                break;
            case GameSceneType.STAGE_2:
                Stage_2Coin();
                break;
            case GameSceneType.STAGE_3:
                Stage_3Coin();
                break;
        }
    }

    void Update()
    {
        // �l�������R�C���� / �X�e�[�W�ɔz�u�����R�C����
        coinNumText.text = $"Coin�F{coinScript.GetPlayerCoin}�^{stageCoins.Length}";

        ChangeSprite();
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

//--------�X�e�[�W���Ƃ̏���--------

    // �X�e�[�W�Z���N�g�ł̏���
    void StageSelectCoin()
    {
        onStageSelect = true;
        
        bool onStage_1 = false;
        bool onStage_2 = false;
        bool onStage_3 = false;
    }

    // �X�e�[�W1�ł̏���
    void Stage_1Coin()
    {
        onStage_1 = true;

        bool onStageSelect = false;
        bool onStage_2 = false;
        bool onStage_3 = false;
    }

    // �X�e�[�W2�ł̏���
    void Stage_2Coin()
    {
        onStage_2 = true;

        bool onStageSelect = false;
        bool onStage_1 = false;
        bool onStage_3 = false;
    }

    // �X�e�[�W3�ł̏���
    void Stage_3Coin()
    {
        onStage_3 = true;

        bool onStageSelect = false;
        bool onStage_1 = false;
        bool onStage_2 = false;
    }

//-------------------------------
}
