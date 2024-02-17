using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----�Q�[���V�[���̃R�C���Ǘ�----
public class CoinManager : SingletonMonoBehaviour<CoinManager>
{
    //-----�V���O���g���p-----
    protected override bool dontDestroyOnLoad { get { return true; } }
    static CoinManager instance;
    //------------------------

    [SerializeField] GameObject[] stageCoins; // �X�e�[�W��ɔz�u����R�C��
    [SerializeField] Sprite coinSprite; // �R�C���摜
    [SerializeField] Image[] coinImages; // UI�Ƃ��ĕ\������R�C��
    [SerializeField] GameObject coinPrefab;

    // �l�������R�C������\������e�L�X�g
    [SerializeField] Text coinNumText;

    Coin coinScript;

    // �X�e�[�W���ɁA�v���C���[���R�C����ێ����Ă��邩�m�F
    bool[] isPlayerCoin_Stage1 = new bool[3] {false, false, false};
    bool[] isPlayerCoin_Stage2 = new bool[3] {false, false, false};
    bool[] isPlayerCoin_Stage3 = new bool[3] {false, false, false};
    bool[] isPlayerCoin_Stage4 = new bool[3] {false, false, false};

    // �v���p�e�B
    public bool[] IsPlayerCoin_Stage1
    {
        get { return isPlayerCoin_Stage1; }
        set { isPlayerCoin_Stage1 = value; }
    }
    public bool[] IsPlayerCoin_Stage2 
    {
        get { return isPlayerCoin_Stage2; }
        set { isPlayerCoin_Stage2 = value; }
    }
    public bool[] IsPlayerCoin_Stage3
    {
        get { return isPlayerCoin_Stage3; }
        set { isPlayerCoin_Stage3 = value; }
    }
    public bool[] IsPlayerCoin_Stage4
    {
        get { return isPlayerCoin_Stage4; }
        set { isPlayerCoin_Stage4 = value; }
    }

    // �ǂ̃V�[���ɂ��邩
    // * <���ӓ_> �uBuildSetting�v�̔ԍ��Ɠ����ɂ���
    public enum GameSceneType
    {
        Title_Scene = 0,
        STAGE_SELECT,
        STAGE_1,
        STAGE_2,
        STAGE_3,
        STAGE_4
    }
    public GameSceneType gameSceneType;
    int currentScene = 0; // ���݂���V�[��

    void Awake()
    {
        CheckInstance(); // �V���O���g��
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject); // �V�[�����ς���Ă����݂���
        coinScript = coinPrefab.GetComponent<Coin>();
        
        // �V�[���̃��[�h����������ƌ��o�����C�x���g
        SceneManager.sceneLoaded += SceneCheck;
    }


    void Update()
    {
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
            // �X�e�[�W4
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

    // �ǂ̃V�[���ɂ��邩
    void SceneCheck(Scene nextScene, LoadSceneMode mode)
    {
        // ���݂̃V�[���ԍ����擾
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"�V�[���ԍ��F{currentScene}");

        switch (currentScene)
        {
            // �^�C�g�����
            case (int)GameSceneType.Title_Scene:
                gameSceneType = GameSceneType.Title_Scene;
                Debug.Log($"�V�[���^�C�v�F{gameSceneType}");
                break;
            // �X�e�[�W�I�����
            case (int)GameSceneType.STAGE_SELECT:
                gameSceneType = GameSceneType.STAGE_SELECT;
                Debug.Log($"�V�[���^�C�v�F{gameSceneType}");
                break;
            // �X�e�[�W1
            case (int)GameSceneType.STAGE_1:
                gameSceneType = GameSceneType.STAGE_1;
                Debug.Log($"�V�[���^�C�v�F{gameSceneType}");
                break;
            // �X�e�[�W2
            case (int)GameSceneType.STAGE_2:
                gameSceneType = GameSceneType.STAGE_2;
                Debug.Log($"�V�[���^�C�v�F{gameSceneType}");
                break;
            // �X�e�[�W3
            case (int)GameSceneType.STAGE_3:
                gameSceneType = GameSceneType.STAGE_3;
                Debug.Log($"�V�[���^�C�v�F{gameSceneType}");
                break;
            // �X�e�[�W4
            case (int)GameSceneType.STAGE_4:
                gameSceneType = GameSceneType.STAGE_4;
                Debug.Log($"�V�[���^�C�v�F{gameSceneType}");
                break;
        }

        // �^�C�g����ʂƃX�e�[�W�I����ʈȊO��
        if (gameSceneType != GameSceneType.Title_Scene
            && gameSceneType != GameSceneType.STAGE_SELECT)
        {
            // (DontDestroyOnLoad�̃P�A)
            // �R�C���e�L�X�g���Q�Ƃ���Ă��Ȃ��Ƃ�
            coinNumText = GameObject.Find("CoinNumText").GetComponent<Text>();
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
        for(int i = 0; i < stageCoins.Length; i++)
        {
            if (stageCoins[i] == null)
            {
                // �R�C�����Q��
                string num = "";
                if(i == 1) num = " (1)";
                else if(i == 2) num = " (2)";
                stageCoins[i] = GameObject.Find("Coin" + num);
                if (isPlayerCoin_Stage1[i])
                {
                    Destroy(stageCoins[i]);
                }
            }
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
            coinImages[1] = GameObject.Find("CoinImage (1)").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage (2)").GetComponent<Image>();
        }

        // (DontDestroyOnLoad�̃P�A)
        // �v���C���[���擾���Ă��Ȃ����A�R�C�����Q�Ƃ���Ă��Ȃ��Ƃ�
        for (int i = 0; i < stageCoins.Length; i++)
        {
            if (stageCoins[i] == null)
            {
                // �R�C�����Q��
                string num = "";
                if (i == 1) num = " (1)";
                else if (i == 2) num = " (2)";
                stageCoins[i] = GameObject.Find("Coin" + num);
                if (isPlayerCoin_Stage2[i])
                {
                    Destroy(stageCoins[i]);
                }
            }
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
            coinImages[1] = GameObject.Find("CoinImage (1)").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage (2)").GetComponent<Image>();
        }

        // (DontDestroyOnLoad�̃P�A)
        // �v���C���[���擾���Ă��Ȃ����A�R�C�����Q�Ƃ���Ă��Ȃ��Ƃ�
        for (int i = 0; i < stageCoins.Length; i++)
        {
            if (stageCoins[i] == null)
            {
                // �R�C�����Q��
                string num = "";
                if (i == 1) num = " (1)";
                else if (i == 2) num = " (2)";
                stageCoins[i] = GameObject.Find("Coin" + num);
                if (isPlayerCoin_Stage3[i])
                {
                    Destroy(stageCoins[i]);
                }
            }
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
            coinImages[1] = GameObject.Find("CoinImage (1)").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage (2)").GetComponent<Image>();
        }

        // (DontDestroyOnLoad�̃P�A)
        // �v���C���[���擾���Ă��Ȃ����A�R�C�����Q�Ƃ���Ă��Ȃ��Ƃ�
        for (int i = 0; i < stageCoins.Length; i++)
        {
            if (stageCoins[i] == null)
            {
                // �R�C�����Q��
                string num = "";
                if (i == 1) num = " (1)";
                else if (i == 2) num = " (2)";
                stageCoins[i] = GameObject.Find("Coin" + num);
                if (isPlayerCoin_Stage4[i])
                {
                    Destroy(stageCoins[i]);
                }
            }
        }
    }

    //------------------------------------------

    // �R�C���摜�ɕύX����
    void ChangeSprite()
    {
        // �^�C�g����ʂƃX�e�[�W�I����ʈȊO��
        if (gameSceneType != GameSceneType.Title_Scene 
            && gameSceneType != GameSceneType.STAGE_SELECT)
        {
        //--------�X�e�[�W���̃R�C���̏�Ԃ��`�F�b�N--------

            // �X�e�[�W1
            if(gameSceneType == GameSceneType.STAGE_1)
            {
                for (int i = 0; i < isPlayerCoin_Stage1.Length; i++)
                {
                    // �X�e�[�W�ɐݒu�����R�C���������Ȃ�����
                    if (!stageCoins[i])
                    {
                        isPlayerCoin_Stage1[i] = true;
                    }
                    // �R�C�����擾���ꂽ��
                    if (isPlayerCoin_Stage1[i])
                    {
                        coinImages[i].sprite = coinSprite;
                    }
                }
            }

            // �X�e�[�W2
            if (gameSceneType == GameSceneType.STAGE_2)
            {
                for (int i = 0; i < isPlayerCoin_Stage2.Length; i++)
                {
                    // �X�e�[�W�ɐݒu�����R�C���������Ȃ�����
                    if (!stageCoins[i])
                    {
                        isPlayerCoin_Stage2[i] = true;
                    }
                    // �R�C�����擾���ꂽ��
                    if (isPlayerCoin_Stage2[i])
                    {
                        coinImages[i].sprite = coinSprite;
                    }
                }
            }

            // �X�e�[�W3
            if (gameSceneType == GameSceneType.STAGE_3)
            {
                for (int i = 0; i < isPlayerCoin_Stage3.Length; i++)
                {
                    // �X�e�[�W�ɐݒu�����R�C���������Ȃ�����
                    if (!stageCoins[i])
                    {
                        isPlayerCoin_Stage3[i] = true;
                    }
                    // �R�C�����擾���ꂽ��
                    if (isPlayerCoin_Stage3[i])
                    {
                        coinImages[i].sprite = coinSprite;
                    }
                }
            }

            // �X�e�[�W4
            if (gameSceneType == GameSceneType.STAGE_4)
            {
                for (int i = 0; i < isPlayerCoin_Stage4.Length; i++)
                {
                    // �X�e�[�W�ɐݒu�����R�C���������Ȃ�����
                    if (!stageCoins[i])
                    {
                        isPlayerCoin_Stage4[i] = true;
                    }
                    // �R�C�����擾���ꂽ��
                    if (isPlayerCoin_Stage4[i])
                    {
                        coinImages[i].sprite = coinSprite;
                    }
                }
            }
        //--------------------------------------------------
        }
    }
}
