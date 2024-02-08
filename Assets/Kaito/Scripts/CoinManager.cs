using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----ゲームシーンのコイン管理----
public class CoinManager : SingletonMonoBehaviour<CoinManager>
{
    protected override bool dontDestroyOnLoad { get { return true; } }
    
    [SerializeField] GameObject[] stageCoins; // ステージ上に配置するコイン
    [SerializeField] Sprite coinSprite; // コイン画像
    [SerializeField] Image[] coinImages; // UIとして表示するコイン
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject uiCanvas;

    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    Coin coinScript;
    MainGameUI mainUIScript;

    // プレイヤーがコインを保持しているか
    [SerializeField]public bool[] Got = new bool[3];

    // どのシーンにいるか
    public enum GameSceneType
    {
        STAGE_SELECT = 0,
        STAGE_1,
        STAGE_2,
        STAGE_3
    }
    public GameSceneType gameSceneType;

    // シングルトン
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
        if (uiCanvas != null) // 参照エラー回避
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
        if (coinNumText != null) // 参照エラー回避
        {
            // ステージごとにコイン枚数を取得
            switch (gameSceneType)
            {
                // ステージ1
                case GameSceneType.STAGE_1:
                    // 獲得したコイン数 / ステージに配置したコイン数
                    coinNumText.text = $"コイン : {coinScript.PlayerCoin_Stage1} / {stageCoins.Length}";
                    break;
                // ステージ2
                case GameSceneType.STAGE_2:
                    coinNumText.text = $"コイン : {coinScript.PlayerCoin_Stage2} / {stageCoins.Length}";
                    break;
                // ステージ3
                case GameSceneType.STAGE_3:
                    coinNumText.text = $"コイン : {coinScript.PlayerCoin_Stage3} / {stageCoins.Length}";
                    break;
            }
        }
        
        ChangeSprite();
    }

    // シングルトン
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

    // コイン画像に変更する
    void ChangeSprite()
    {
        // ステージ選択画面以外で
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
