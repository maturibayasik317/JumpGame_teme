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
    [SerializeField] GameObject uiCanvas;

    // �l�������R�C������\������e�L�X�g
    [SerializeField] Text coinNumText;

    Coin coinScript;
    MainGameUI mainUIScript;

    // �v���C���[���R�C����ێ����Ă��邩
    [SerializeField]public bool[] Got = new bool[3];

    // �ǂ̃V�[���ɂ��邩
    public enum GameSceneType
    {
        STAGE_SELECT = 0,
        STAGE_1,
        STAGE_2,
        STAGE_3
    }
    public GameSceneType gameSceneType;

    // �V���O���g��
    static CoinManager instance;

    void Awake()
    {
        Got[0] = false;
        Got[1] = false;
        Got[2] = false;
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (uiCanvas != null) // �Q�ƃG���[���
        {
            mainUIScript = uiCanvas.GetComponent<MainGameUI>();
        }
        coinScript = coinPrefab.GetComponent<Coin>();
    }


    void Update()
    {
        if (!coinImages[0]) {
            coinImages[0] = GameObject.Find("CoinImage").GetComponent<Image>();
        }
        if (Got[0]==false && !stageCoins[0])
        {
            stageCoins[0] = GameObject.Find("Coin");
        }
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

    // �R�C���摜�ɕύX����
    void ChangeSprite()
    {
        // �X�e�[�W�I����ʈȊO��
        if (gameSceneType != GameSceneType.STAGE_SELECT)
        {
            if (!stageCoins[0])
            {
                Got[0] = true;
            }
            if (!stageCoins[1])
            {
                Got[1] = true;
                coinImages[1].sprite = coinSprite;
            }
            if (!stageCoins[2])
            {
                Got[2] = true;
                coinImages[2].sprite = coinSprite;
            }
            if (Got[0]) { 
                coinImages[0].sprite = coinSprite;
            }
        }
    }
}
