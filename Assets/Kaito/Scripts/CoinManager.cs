using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----ゲームシーンのコイン管理----
public class CoinManager : SingletonMonoBehaviour<CoinManager>
{
    //-----シングルトン用-----
    protected override bool dontDestroyOnLoad { get { return true; } }
    static CoinManager instance;
    //------------------------

    [SerializeField] GameObject[] stageCoins; // ステージ上に配置するコイン
    [SerializeField] Sprite coinSprite; // コイン画像
    [SerializeField] Image[] coinImages; // UIとして表示するコイン
    [SerializeField] GameObject coinPrefab;

    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    Coin coinScript;

    // ステージ毎に、プレイヤーがコインを保持しているか確認
    bool[] isPlayerCoin_Stage1 = new bool[3] {false, false, false};
    bool[] isPlayerCoin_Stage2 = new bool[3] {false, false, false};
    bool[] isPlayerCoin_Stage3 = new bool[3] {false, false, false};
    bool[] isPlayerCoin_Stage4 = new bool[3] {false, false, false};

    // プロパティ
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
        CheckInstance(); // シングルトン
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject); // シーンが変わっても存在する
        coinScript = coinPrefab.GetComponent<Coin>();
        
        // シーンのロードが完了すると検出されるイベント
        SceneManager.sceneLoaded += SceneCheck;
    }


    void Update()
    {
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
    void SceneCheck(Scene nextScene, LoadSceneMode mode)
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

        // タイトル画面とステージ選択画面以外で
        if (gameSceneType != GameSceneType.Title_Scene
            && gameSceneType != GameSceneType.STAGE_SELECT)
        {
            // (DontDestroyOnLoadのケア)
            // コインテキストが参照されていないとき
            coinNumText = GameObject.Find("CoinNumText").GetComponent<Text>();
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
        for(int i = 0; i < stageCoins.Length; i++)
        {
            if (stageCoins[i] == null)
            {
                // コインを参照
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
        for (int i = 0; i < stageCoins.Length; i++)
        {
            if (stageCoins[i] == null)
            {
                // コインを参照
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
        for (int i = 0; i < stageCoins.Length; i++)
        {
            if (stageCoins[i] == null)
            {
                // コインを参照
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
        for (int i = 0; i < stageCoins.Length; i++)
        {
            if (stageCoins[i] == null)
            {
                // コインを参照
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

    // コイン画像に変更する
    void ChangeSprite()
    {
        // タイトル画面とステージ選択画面以外で
        if (gameSceneType != GameSceneType.Title_Scene 
            && gameSceneType != GameSceneType.STAGE_SELECT)
        {
        //--------ステージ毎のコインの状態をチェック--------

            // ステージ1
            if(gameSceneType == GameSceneType.STAGE_1)
            {
                for (int i = 0; i < isPlayerCoin_Stage1.Length; i++)
                {
                    // ステージに設置したコインが無くなったら
                    if (!stageCoins[i])
                    {
                        isPlayerCoin_Stage1[i] = true;
                    }
                    // コインが取得されたら
                    if (isPlayerCoin_Stage1[i])
                    {
                        coinImages[i].sprite = coinSprite;
                    }
                }
            }

            // ステージ2
            if (gameSceneType == GameSceneType.STAGE_2)
            {
                for (int i = 0; i < isPlayerCoin_Stage2.Length; i++)
                {
                    // ステージに設置したコインが無くなったら
                    if (!stageCoins[i])
                    {
                        isPlayerCoin_Stage2[i] = true;
                    }
                    // コインが取得されたら
                    if (isPlayerCoin_Stage2[i])
                    {
                        coinImages[i].sprite = coinSprite;
                    }
                }
            }

            // ステージ3
            if (gameSceneType == GameSceneType.STAGE_3)
            {
                for (int i = 0; i < isPlayerCoin_Stage3.Length; i++)
                {
                    // ステージに設置したコインが無くなったら
                    if (!stageCoins[i])
                    {
                        isPlayerCoin_Stage3[i] = true;
                    }
                    // コインが取得されたら
                    if (isPlayerCoin_Stage3[i])
                    {
                        coinImages[i].sprite = coinSprite;
                    }
                }
            }

            // ステージ4
            if (gameSceneType == GameSceneType.STAGE_4)
            {
                for (int i = 0; i < isPlayerCoin_Stage4.Length; i++)
                {
                    // ステージに設置したコインが無くなったら
                    if (!stageCoins[i])
                    {
                        isPlayerCoin_Stage4[i] = true;
                    }
                    // コインが取得されたら
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
