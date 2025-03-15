using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 複数のクラスで使用する定数を定義したクラス
/// </summary>
public static class GlobalConst
{
    public const int MAX_STAGE_NUM = 4; // ステージの最大数
    public const int MAX_COIN_NUM = 3;  // コインの最大枚数
}

/// <summary>
/// 各ステージのコイン状況を管理するクラス
/// </summary>
public class CoinManager : MonoBehaviour
{
    // シングルトン用
    static CoinManager instance = null;

    [SerializeField] GameObject[] stageCoins; // ステージ上に配置するコイン
    [SerializeField] Sprite coinSprite; // コイン画像
    [SerializeField] Image[] coinImages; // UIとして表示するコイン
    [SerializeField] GameObject coinPrefab;

    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    Coin coinScript;

    // ステージ毎に、プレイヤーがコインを保持しているか確認：削除予定
    bool[] isPlayerCoin_Stage1 = new bool[3] {false, false, false};
    bool[] isPlayerCoin_Stage2 = new bool[3] {false, false, false};
    bool[] isPlayerCoin_Stage3 = new bool[3] {false, false, false};
    bool[] isPlayerCoin_Stage4 = new bool[3] {false, false, false};

    // ステージごとに、プレイヤーが各コインを保持しているかどうか
    bool[,] playerHaveCoins = new bool[GlobalConst.MAX_STAGE_NUM, GlobalConst.MAX_COIN_NUM] {
        { false, false, false },
        { false, false, false },
        { false, false, false },
        { false, false, false }
    };

    /// <summary>
    /// プレイヤーが取得しているコインの情報を返す関数
    /// </summary>
    /// <param name="_stageNum">ステージ番号</param>
    /// <param name="_coinNum">コイン番号</param>
    /// <returns>引数で指定した、nステージ目のn枚目のコインを取得していればtrue</returns>
    public bool GetPlayerHaveCoins(int _stageNum, int _coinNum) { return playerHaveCoins[_stageNum, _coinNum]; }

    /// <summary>
    /// プレイヤーが取得しているコインの情報をリセットする関数
    /// </summary>
    /// <param name="_stageNum">ステージ番号</param>
    public void ResetPlayerHaveCoins(int _stageNum)
    {
        for (int sNum = 0; sNum < _stageNum; sNum++)
        {
            for (int cNum = 0; cNum < stageCoins.Length; cNum++)
            {
                playerHaveCoins[sNum , cNum] = false;
            }
        }
    }

    // プロパティ：削除予定
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
        TITLE = 0,
        STAGE_SELECT,
        STAGE_1,
        STAGE_2,
        STAGE_3,
        STAGE_4
    }
    public GameSceneType gameSceneType;
    int stageNum = 0; // 現在いるステージ
    /// <summary>
    /// ステージ番号を返す関数
    /// </summary>
    /// <returns>現在のステージ番号</returns>
    public int GetStageNum() { return stageNum; }

    void Awake()
    {
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        coinScript = coinPrefab.GetComponent<Coin>();
        
        // シーンのロードが完了すると検出されるイベント
        SceneManager.sceneLoaded += SceneCheck;
    }


    void Update()
    {
        #if false // 変更前
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
        #endif

        // タイトル画面かステージ選択画面なら処理しない
        if (gameSceneType == GameSceneType.TITLE || gameSceneType == GameSceneType.STAGE_SELECT) return;
        
        CoinUI();
        ChangeSprite();
    }

    /// <summary>
    /// シングルトン
    /// </summary>
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

    /// <summary>
    /// 現在のシーンを取得する関数
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    void SceneCheck(Scene nextScene, LoadSceneMode mode)
    {
        stageNum = 0; // ここで初期化しておく

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            // タイトル画面
            case (int)GameSceneType.TITLE:
                gameSceneType = GameSceneType.TITLE;
                break;
            // ステージ選択画面
            case (int)GameSceneType.STAGE_SELECT:
                gameSceneType = GameSceneType.STAGE_SELECT;
                break;
            // ステージ1
            case (int)GameSceneType.STAGE_1:
                gameSceneType = GameSceneType.STAGE_1;
                stageNum = 1;
                break;
            // ステージ2
            case (int)GameSceneType.STAGE_2:
                gameSceneType = GameSceneType.STAGE_2;
                stageNum = 2;
                break;
            // ステージ3
            case (int)GameSceneType.STAGE_3:
                gameSceneType = GameSceneType.STAGE_3;
                stageNum = 3;
                break;
            // ステージ4
            case (int)GameSceneType.STAGE_4:
                gameSceneType = GameSceneType.STAGE_4;
                stageNum = 4;
                break;
        }

        // タイトル画面とステージ選択画面以外で
        if (gameSceneType != GameSceneType.TITLE
            && gameSceneType != GameSceneType.STAGE_SELECT)
        {
            // (DontDestroyOnLoadのケア)
            // コインテキストが参照されていないとき
            coinNumText = GameObject.Find("CoinNumText").GetComponent<Text>();
        }
    }

    /// <summary>
    /// ステージに配置するコインを設定する関数
    /// </summary>
    void CoinUI()
    {
        coinNumText.text = $"コイン： {coinScript.GetPlayerCoinNums(stageNum)} / {stageCoins.Length}";

        for (int num = 0; num < stageCoins.Length; num++)
        {
            if (coinImages[num] != null) return;

            coinImages[num] = GameObject.Find("CoinImage_" + num).GetComponent<Image>();
        }
        
        for (int num = 0; num < stageCoins.Length; num++)
        {
            if (stageCoins[num] != null) return;

            // コインを参照
            stageCoins[num] = GameObject.Find("Coin_" + num);
            if (playerHaveCoins[stageNum - 1, num]) // 既に取得済みのコインは消す
            {
                Destroy(stageCoins[num]);
            }
        }
    }

    /*↓-----------------------------変更前-----------------------------↓*/
    
    void CoinUIStage_1() // ステージ1
    {
        // 獲得したコイン数 / ステージに配置したコイン数
        coinNumText.text = $"コイン : {coinScript.PlayerCoin_Stage1} / {stageCoins.Length}";

        // (DontDestroyOnLoadのケア)
        for (int i = 0; i < coinImages.Length; i++)
        {
            if (coinImages[i] == null)
            {
                coinImages[i] = GameObject.Find("CoinImage_" + i).GetComponent<Image>();
            }
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
    
    /*↑-----------------------------変更前-----------------------------↑*/

    /// <summary>
    /// 各ステージのコインUI画像を変更する関数
    /// </summary>
    void ChangeSprite()
    {
        // ステージごとのコインの状態をチェック
        for (int num = 0; num < stageCoins.Length; num++)
        {
            // ステージに設置したコインが無くなったら
            if (!stageCoins[num])
            {
                playerHaveCoins[stageNum - 1, num] = true;
            }
            // コインが取得されたら
            if (playerHaveCoins[stageNum - 1, num])
            {
                coinImages[num].sprite = coinSprite;
            }
        }
    }

#if false // 変更前
    /// <summary>
    /// コイン画像に変更する関数
    /// </summary>
    void ChangeSprite()
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
    }
#endif
}
