using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----ゲームシーンのコイン管理----
public class CoinManager : MonoBehaviour
{
    [SerializeField] GameObject[] stageCoins; // ステージ上に配置するコイン
    [SerializeField] Sprite coinSprite; // コイン画像
    [SerializeField] Image[] coinImages; // UIとして表示するコイン
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject uiCanvas;

    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    Coin coinScript;
    MainGameUI mainUIScript;

    // どのシーンにいるか
    public enum GameSceneType
    {
        STAGE_SELECT = 0,
        STAGE_1,
        STAGE_2,
        STAGE_3
    }
    public GameSceneType gameSceneType;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (uiCanvas != null) // 参照エラー回避
        {
            mainUIScript = uiCanvas.GetComponent<MainGameUI>();
        }
        coinScript = coinPrefab.GetComponent<Coin>();
    }

    void Update()
    {
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

    // コイン画像に変更する
    void ChangeSprite()
    {
        // ステージ選択画面以外で
        if (gameSceneType != GameSceneType.STAGE_SELECT)
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
    }

    //--------ステージごとの処理--------

    // ステージセレクトでの処理
    //void StageSelectCoin()
    //{
        
    //}

    //// ステージ1での処理
    //void Stage_1Coin()
    //{
        
    //}

    //// ステージ2での処理
    //void Stage_2Coin()
    //{
        
    //}

    //// ステージ3での処理
    //void Stage_3Coin()
    //{
        
    //}

    //-------------------------------
}
