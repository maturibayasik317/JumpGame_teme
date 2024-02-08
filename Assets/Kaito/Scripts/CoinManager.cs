using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----�Q�[���V�[���̃R�C���Ǘ�----
public class CoinManager : SingletonMonoBehaviour<CoinManager>
{
    protected override bool dontDestroyOnLoad { get { return true; } }
    
    [SerializeField] GameObject[] stageCoins; // �X�e�[�W��ɔz�u����R�C��
    [SerializeField] Sprite coinSprite; // �R�C���摜
    [SerializeField] Image[] coinImages; // UI�Ƃ��ĕ\������R�C��
    [SerializeField] GameObject coinPrefab;

    // �l�������R�C������\������e�L�X�g
    [SerializeField] Text coinNumText;

    Coin coinScript;

    // �X�e�[�W���ɁA�v���C���[���R�C����ێ����Ă��邩�m�F
    [SerializeField] public bool[] isPlayerCoin_Stage1 = new bool[3];
    [SerializeField] public bool[] isPlayerCoin_Stage2 = new bool[3];
    [SerializeField] public bool[] isPlayerCoin_Stage3 = new bool[3];
    [SerializeField] public bool[] isPlayerCoin_Stage4 = new bool[3];

    // �ǂ̃V�[���ɂ��邩
    public enum GameSceneType
    {
        STAGE_SELECT = 0,
        STAGE_1,
        STAGE_2,
        STAGE_3,
        STAGE_4
    }
    public GameSceneType gameSceneType;

    // �V���O���g��
    static CoinManager instance;

    void Awake()
    {
        for (int i = 0; i < isPlayerCoin_Stage1.Length; i++)
        {
            isPlayerCoin_Stage1[i] = false;
        }
        //isPlayer_Stage1[0] = false;
        //isPlayer_Stage1[1] = false;
        //isPlayer_Stage1[2] = false;
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        coinScript = coinPrefab.GetComponent<Coin>();
    }


    void Update()
    {
        // (DontDestroyOnLoad�̃P�A)
        // �X�e�[�W�I����ʈȊO�ŁA�R�C���e�L�X�g���Q�Ƃ���Ă��Ȃ��Ƃ�
        if (gameSceneType != GameSceneType.STAGE_SELECT && coinNumText == null)
        {
            // �e�L�X�g�݂̂��擾
            coinNumText = GameObject.Find("CoinNumText").GetComponent<Text>();
        }

        // �X�e�[�W���ƂɃR�C���������擾
        switch (gameSceneType)
        {
            // �X�e�[�W1
            case GameSceneType.STAGE_1:
                CoinUIStage_1();
                break;
            // �X�e�[�W2
            case GameSceneType.STAGE_2:
                CoinUIStage_2();
                break;
            // �X�e�[�W3
            case GameSceneType.STAGE_3:
                CoinUIStage_3();
                break;
            // �X�e�[�W�S
            case GameSceneType.STAGE_4:
                CoinUIStage_4();
                break;
         
        }
        
        ChangeSprite();
    }

    // �V���O���g��
    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //-----�X�e�[�W���̃R�C���֘A��UI�̏���-----
    
    void CoinUIStage_1() // �X�e�[�W1
    {
        // �l�������R�C���� / �X�e�[�W�ɔz�u�����R�C����
        coinNumText.text = $"�R�C�� : {coinScript.PlayerCoin_Stage1} / {stageCoins.Length}";

        // (DontDestroyOnLoad�̃P�A)
        // �R�C���̉摜��Ԃ�null�ɂȂ�����
        if (coinImages[0] == null)
        {
            // �R�C���摜��Ԃ��擾
            coinImages[0] = GameObject.Find("CoinImage").GetComponent<Image>();
        }
        if (coinImages[1] == null)
        {
            coinImages[1] = GameObject.Find("CoinImage (1)").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage (2)").GetComponent<Image>();
        }
        
        // (DontDestroyOnLoad�̃P�A)
        // �v���C���[���擾���Ă��Ȃ����A�R�C�����Q�Ƃ���Ă��Ȃ��Ƃ�
        if (!isPlayerCoin_Stage1[0] && stageCoins[0] == null)
        {
            // �R�C�����Q��
            stageCoins[0] = GameObject.Find("Coin");
        }
        if (!isPlayerCoin_Stage1[1] && stageCoins[1] == null)
        {
            stageCoins[1] = GameObject.Find("Coin (1)");
        }
        if (!isPlayerCoin_Stage1[2] && stageCoins[2] == null)
        {
            stageCoins[2] = GameObject.Find("Coin (2)");
        }
    }
    void CoinUIStage_2() // �X�e�[�W2
    {
        // �l�������R�C���� / �X�e�[�W�ɔz�u�����R�C����
        coinNumText.text = $"�R�C�� : {coinScript.PlayerCoin_Stage2} / {stageCoins.Length}";

        // (DontDestroyOnLoad�̃P�A)
        // �R�C���̉摜��Ԃ�null�ɂȂ�����
        if (coinImages[0] == null)
        {
            // �R�C���摜��Ԃ��擾
            coinImages[0] = GameObject.Find("CoinImage").GetComponent<Image>();
        }
        if (coinImages[1] == null)
        {
            coinImages[1] = GameObject.Find("CoinImage").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage").GetComponent<Image>();
        }

        // (DontDestroyOnLoad�̃P�A)
        // �v���C���[���擾���Ă��Ȃ����A�R�C�����Q�Ƃ���Ă��Ȃ��Ƃ�
        if (!isPlayerCoin_Stage2[0] && stageCoins[0] == null)
        {
            // �R�C�����Q��
            stageCoins[0] = GameObject.Find("Coin");
        }
        if (!isPlayerCoin_Stage2[1] && stageCoins[1] == null)
        {
            stageCoins[1] = GameObject.Find("Coin");
        }
        if (!isPlayerCoin_Stage2[2] && stageCoins[2] == null)
        {
            stageCoins[2] = GameObject.Find("Coin");
        }
    }
    void CoinUIStage_3() // �X�e�[�W3
    {
        // �l�������R�C���� / �X�e�[�W�ɔz�u�����R�C����
        coinNumText.text = $"�R�C�� : {coinScript.PlayerCoin_Stage3} / {stageCoins.Length}";

        // (DontDestroyOnLoad�̃P�A)
        // �R�C���̉摜��Ԃ�null�ɂȂ�����
        if (coinImages[0] == null)
        {
            // �R�C���摜��Ԃ��擾
            coinImages[0] = GameObject.Find("CoinImage").GetComponent<Image>();
        }
        if (coinImages[1] == null)
        {
            coinImages[1] = GameObject.Find("CoinImage").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage").GetComponent<Image>();
        }

        // (DontDestroyOnLoad�̃P�A)
        // �v���C���[���擾���Ă��Ȃ����A�R�C�����Q�Ƃ���Ă��Ȃ��Ƃ�
        if (!isPlayerCoin_Stage3[0] && stageCoins[0] == null)
        {
            // �R�C�����Q��
            stageCoins[0] = GameObject.Find("Coin");
        }
        if (!isPlayerCoin_Stage3[1] && stageCoins[1] == null)
        {
            stageCoins[1] = GameObject.Find("Coin");
        }
        if (!isPlayerCoin_Stage3[2] && stageCoins[2] == null)
        {
            stageCoins[2] = GameObject.Find("Coin");
        }
    }
    void CoinUIStage_4() // �X�e�[�W4
    {
        // �l�������R�C���� / �X�e�[�W�ɔz�u�����R�C����
        coinNumText.text = $"�R�C�� : {coinScript.PlayerCoin_Stage4} / {stageCoins.Length}";

        // (DontDestroyOnLoad�̃P�A)
        // �R�C���̉摜��Ԃ�null�ɂȂ�����
        if (coinImages[0] == null)
        {
            // �R�C���摜��Ԃ��擾
            coinImages[0] = GameObject.Find("CoinImage").GetComponent<Image>();
        }
        if (coinImages[1] == null)
        {
            coinImages[1] = GameObject.Find("CoinImage").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage").GetComponent<Image>();
        }

        // (DontDestroyOnLoad�̃P�A)
        // �v���C���[���擾���Ă��Ȃ����A�R�C�����Q�Ƃ���Ă��Ȃ��Ƃ�
        if (!isPlayerCoin_Stage4[0] && stageCoins[0] == null)
        {
            // �R�C�����Q��
            stageCoins[0] = GameObject.Find("Coin");
        }
        if (!isPlayerCoin_Stage4[1] && stageCoins[1] == null)
        {
            stageCoins[1] = GameObject.Find("Coin");
        }
        if (!isPlayerCoin_Stage4[2] && stageCoins[2] == null)
        {
            stageCoins[2] = GameObject.Find("Coin");
        }
    }

    //------------------------------------------

    // �R�C���摜�ɕύX����
    void ChangeSprite()
    {
        // �X�e�[�W�I����ʈȊO��
        if (gameSceneType != GameSceneType.STAGE_SELECT)
        {
            // �X�e�[�W�ɐݒu�����R�C���������Ȃ�����
            if (!stageCoins[0])
            {
                // �擾��Ԃ�true��
                isPlayerCoin_Stage1[0] = true;
            }
            if (!stageCoins[1])
            {
                isPlayerCoin_Stage1[1] = true;
            }
            if (!stageCoins[2])
            {
                isPlayerCoin_Stage1[2] = true;
            }

            //------�~4-----------
            // �R�C�����擾���ꂽ��
            if (isPlayerCoin_Stage1[0])
            {
                // UI�ɉ摜��\��
                coinImages[0].sprite = coinSprite;
            }
            if (isPlayerCoin_Stage1[1])
            {
                coinImages[1].sprite = coinSprite;
            }
            if (isPlayerCoin_Stage1[2])
            {
                coinImages[2].sprite = coinSprite;
            }
        }
    }
}
