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

    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    Coin coinScript;

    // ステージ毎に、プレイヤーがコインを保持しているか確認
    [SerializeField] public bool[] isPlayerCoin_Stage1 = new bool[3];
    [SerializeField] public bool[] isPlayerCoin_Stage2 = new bool[3];
    [SerializeField] public bool[] isPlayerCoin_Stage3 = new bool[3];
    [SerializeField] public bool[] isPlayerCoin_Stage4 = new bool[3];

    // どのシーンにいるか
    public enum GameSceneType
    {
        STAGE_SELECT = 0,
        STAGE_1,
        STAGE_2,
        STAGE_3,
        STAGE_4
    }
    public GameSceneType gameSceneType;

    // シングルトン
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
        // (DontDestroyOnLoadのケア)
        // ステージ選択画面以外で、コインテキストが参照されていないとき
        if (gameSceneType != GameSceneType.STAGE_SELECT && coinNumText == null)
        {
            // テキストのみを取得
            coinNumText = GameObject.Find("CoinNumText").GetComponent<Text>();
        }

        // ステージごとにコイン枚数を取得
        switch (gameSceneType)
        {
            // ステージ1
            case GameSceneType.STAGE_1:
                CoinUIStage_1();
                break;
            // ステージ2
            case GameSceneType.STAGE_2:
                CoinUIStage_2();
                break;
            // ステージ3
            case GameSceneType.STAGE_3:
                CoinUIStage_3();
                break;
            // ステージ４
            case GameSceneType.STAGE_4:
                CoinUIStage_4();
                break;
         
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

    //-----ステージ毎のコイン関連のUIの処理-----
    
    void CoinUIStage_1() // ステージ1
    {
        // 獲得したコイン数 / ステージに配置したコイン数
        coinNumText.text = $"コイン : {coinScript.PlayerCoin_Stage1} / {stageCoins.Length}";

        // (DontDestroyOnLoadのケア)
        // コインの画像状態がnullになったら
        if (coinImages[0] == null)
        {
            // コイン画像状態を取得
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
        
        // (DontDestroyOnLoadのケア)
        // プレイヤーが取得していないかつ、コインが参照されていないとき
        if (!isPlayerCoin_Stage1[0] && stageCoins[0] == null)
        {
            // コインを参照
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
    void CoinUIStage_2() // ステージ2
    {
        // 獲得したコイン数 / ステージに配置したコイン数
        coinNumText.text = $"コイン : {coinScript.PlayerCoin_Stage2} / {stageCoins.Length}";

        // (DontDestroyOnLoadのケア)
        // コインの画像状態がnullになったら
        if (coinImages[0] == null)
        {
            // コイン画像状態を取得
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

        // (DontDestroyOnLoadのケア)
        // プレイヤーが取得していないかつ、コインが参照されていないとき
        if (!isPlayerCoin_Stage2[0] && stageCoins[0] == null)
        {
            // コインを参照
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
    void CoinUIStage_3() // ステージ3
    {
        // 獲得したコイン数 / ステージに配置したコイン数
        coinNumText.text = $"コイン : {coinScript.PlayerCoin_Stage3} / {stageCoins.Length}";

        // (DontDestroyOnLoadのケア)
        // コインの画像状態がnullになったら
        if (coinImages[0] == null)
        {
            // コイン画像状態を取得
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

        // (DontDestroyOnLoadのケア)
        // プレイヤーが取得していないかつ、コインが参照されていないとき
        if (!isPlayerCoin_Stage3[0] && stageCoins[0] == null)
        {
            // コインを参照
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
    void CoinUIStage_4() // ステージ4
    {
        // 獲得したコイン数 / ステージに配置したコイン数
        coinNumText.text = $"コイン : {coinScript.PlayerCoin_Stage4} / {stageCoins.Length}";

        // (DontDestroyOnLoadのケア)
        // コインの画像状態がnullになったら
        if (coinImages[0] == null)
        {
            // コイン画像状態を取得
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

        // (DontDestroyOnLoadのケア)
        // プレイヤーが取得していないかつ、コインが参照されていないとき
        if (!isPlayerCoin_Stage4[0] && stageCoins[0] == null)
        {
            // コインを参照
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

    // コイン画像に変更する
    void ChangeSprite()
    {
        // ステージ選択画面以外で
        if (gameSceneType != GameSceneType.STAGE_SELECT)
        {
            // ステージに設置したコインが無くなったら
            if (!stageCoins[0])
            {
                // 取得状態をtrueに
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

            //------×4-----------
            // コインが取得されたら
            if (isPlayerCoin_Stage1[0])
            {
                // UIに画像を表示
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
