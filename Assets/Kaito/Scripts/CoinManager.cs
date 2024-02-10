using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----ゲームシーンのコイン管理----
public class CoinManager : SingletonMonoBehaviour<CoinManager>
{
    // シングルトン
    protected override bool dontDestroyOnLoad { get { return true; } }
    static CoinManager instance;

    [SerializeField] GameObject[] stageCoins; // ステージ上に配置するコイン
    [SerializeField] Sprite coinSprite; // コイン画像
    [SerializeField] Image[] coinImages; // UIとして表示するコイン
    [SerializeField] GameObject coinPrefab;

    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    Coin coinScript;

    // ステージ毎に、プレイヤーがコインを保持しているか確認
    [SerializeField] public bool[] isPlayerCoin_Stage1 = new bool[3] {false, false, false};
    [SerializeField] public bool[] isPlayerCoin_Stage2 = new bool[3] {false, false, false};
    [SerializeField] public bool[] isPlayerCoin_Stage3 = new bool[3] {false, false, false};
    [SerializeField] public bool[] isPlayerCoin_Stage4 = new bool[3] {false, false, false};

    // プロパティ
    public bool[] IsPlaerCoin_Stage1
    {
        get { return isPlayerCoin_Stage1; }
        set { isPlayerCoin_Stage1 = value; }
    }
    public bool[] IsPlaerCoin_Stage2 
    {
        get { return isPlayerCoin_Stage2; }
        set { isPlayerCoin_Stage2 = value; }
    }
    public bool[] IsPlaerCoin_Stage3
    {
        get { return isPlayerCoin_Stage3; }
        set { isPlayerCoin_Stage3 = value; }
    }
    public bool[] IsPlaerCoin_Stage4
    {
        get { return isPlayerCoin_Stage4; }
        set { isPlayerCoin_Stage4 = value; }
    }

    // どのシーンにいるか
    // * <注意点> 「BuildSetting」の番号と同じにする
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
    int currentScene = 0; // 現在いるシーン

    void Awake()
    {
        CheckInstance();
        SceneCheck();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        coinScript = coinPrefab.GetComponent<Coin>();
    }


    void Update()
    {
        // (DontDestroyOnLoadのケア)
        // タイトル画面とステージ選択画面以外で、コインテキストが参照されていないとき
        if ((gameSceneType != GameSceneType.Title_Scene 
            || gameSceneType != GameSceneType.STAGE_SELECT) && coinNumText == null)
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
            // ステージ4
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

    // どのシーンにいるか
    void SceneCheck()
    {
        // 現在のシーン番号を取得
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"シーン番号：{currentScene}");

        switch (currentScene)
        {
            // タイトル画面
            case (int)GameSceneType.Title_Scene:
                gameSceneType = GameSceneType.Title_Scene;
                Debug.Log($"シーンタイプ：{gameSceneType}");
                break;
            // ステージ選択画面
            case (int)GameSceneType.STAGE_SELECT:
                gameSceneType = GameSceneType.STAGE_SELECT;
                Debug.Log($"シーンタイプ：{gameSceneType}");
                break;
            // ステージ1
            case (int)GameSceneType.STAGE_1:
                gameSceneType = GameSceneType.STAGE_1;
                Debug.Log($"シーンタイプ：{gameSceneType}");
                break;
            // ステージ2
            case (int)GameSceneType.STAGE_2:
                gameSceneType = GameSceneType.STAGE_2;
                Debug.Log($"シーンタイプ：{gameSceneType}");
                break;
            // ステージ3
            case (int)GameSceneType.STAGE_3:
                gameSceneType = GameSceneType.STAGE_3;
                Debug.Log($"シーンタイプ：{gameSceneType}");
                break;
            // ステージ4
            case (int)GameSceneType.STAGE_4:
                gameSceneType = GameSceneType.STAGE_4;
                Debug.Log($"シーンタイプ：{gameSceneType}");
                break;
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
            coinImages[1] = GameObject.Find("CoinImage (1)").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage (2)").GetComponent<Image>();
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
            stageCoins[1] = GameObject.Find("Coin (1)");
        }
        if (!isPlayerCoin_Stage2[2] && stageCoins[2] == null)
        {
            stageCoins[2] = GameObject.Find("Coin (2)");
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
            coinImages[1] = GameObject.Find("CoinImage (1)").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage (2)").GetComponent<Image>();
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
            stageCoins[1] = GameObject.Find("Coin (1)");
        }
        if (!isPlayerCoin_Stage3[2] && stageCoins[2] == null)
        {
            stageCoins[2] = GameObject.Find("Coin (2)");
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
            coinImages[1] = GameObject.Find("CoinImage (1)").GetComponent<Image>();
        }
        if (coinImages[2] == null)
        {
            coinImages[2] = GameObject.Find("CoinImage (2)").GetComponent<Image>();
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
            stageCoins[1] = GameObject.Find("Coin (1)");
        }
        if (!isPlayerCoin_Stage4[2] && stageCoins[2] == null)
        {
            stageCoins[2] = GameObject.Find("Coin (2)");
        }
    }

    //------------------------------------------

    // コイン画像に変更する
    void ChangeSprite()
    {
        // タイトル画面とステージ選択画面以外で
        if (gameSceneType != GameSceneType.Title_Scene 
            || gameSceneType != GameSceneType.STAGE_SELECT)
        {
        //--------ステージ毎のコインの状態をチェック--------

            // ステージ1
            for (int i = 0; i < isPlayerCoin_Stage1.Length; i++)
            {
                // ステージに設置したコインが無くなったら
                if (!stageCoins[i])
                {
                    isPlayerCoin_Stage1[i] = true;
                }
            }
            // ステージ2
            for (int i = 0; i < isPlayerCoin_Stage2.Length; i++)
            {
                // ステージに設置したコインが無くなったら
                if (!stageCoins[i])
                {
                    isPlayerCoin_Stage2[i] = true;
                }
            }
            // ステージ3
            for (int i = 0; i < isPlayerCoin_Stage3.Length; i++)
            {
                // ステージに設置したコインが無くなったら
                if (!stageCoins[i])
                {
                    isPlayerCoin_Stage3[i] = true;
                }
            }
            // ステージ4
            for (int i = 0; i < isPlayerCoin_Stage4.Length; i++)
            {
                // ステージに設置したコインが無くなったら
                if (!stageCoins[i])
                {
                    isPlayerCoin_Stage4[i] = true;
                }
            }

        //--------------------------------------------------

        //-------------ステージ毎の画像表示処理-------------

            // ステージ1
            for (int i = 0; i < isPlayerCoin_Stage1.Length; i++)
            {
                // コインが取得されたら
                if (isPlayerCoin_Stage1[i])
                {
                    coinImages[i].sprite = coinSprite;
                }
            }
            // ステージ2
            for (int i = 0; i < isPlayerCoin_Stage2.Length; i++)
            {
                // コインが取得されたら
                if (isPlayerCoin_Stage2[i])
                {
                    coinImages[i].sprite = coinSprite;
                }
            }
            // ステージ3
            for (int i = 0; i < isPlayerCoin_Stage3.Length; i++)
            {
                // コインが取得されたら
                if (isPlayerCoin_Stage3[i])
                {
                    coinImages[i].sprite = coinSprite;
                }
            }
            // ステージ4
            for (int i = 0; i < isPlayerCoin_Stage4.Length; i++)
            {
                if (isPlayerCoin_Stage4[i])
                {
                    coinImages[i].sprite = coinSprite;
                }
            }

        //--------------------------------------------------

        }
        // ステージ選択画面で
        else if (gameSceneType == GameSceneType.STAGE_SELECT)
        {
            // コイン画像状態を取得する処理
        }
    }
}
