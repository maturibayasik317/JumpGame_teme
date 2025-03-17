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
}
