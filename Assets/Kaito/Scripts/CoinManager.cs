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
    [SerializeField] GameObject coinPrefabs;

    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    Coin coinScript;

    [SerializeField] GameSceneType gameSceneType;
    bool onStageSelect = false;
    bool onStage_1 = false;
    bool onStage_2 = false;
    bool onStage_3 = false;

    // 「Coin.cs」で使用
    public bool GetOnStageSelect => onStageSelect;
    public bool GetOnStage_1 => onStage_1;
    public bool GetOnStage_2 => onStage_2;
    public bool GetOnStage_3 => onStage_3;

    // どのシーンにいるか
    enum GameSceneType
    {
        STAGE_SELECT,
        STAGE_1,
        STAGE_2,
        STAGE_3
    }

    // どのシーンかを整数で渡す
    public int GetGameSceneType => (int)gameSceneType;

    void Start()
    {
        coinScript = coinPrefabs.GetComponent<Coin>();
        
        // ステージごとにコイン数を取得
        switch (gameSceneType)
        {
            case GameSceneType.STAGE_SELECT:
                // 初期化処理
                StageSelectCoin();
                break;
            case GameSceneType.STAGE_1:
                Stage_1Coin();
                break;
            case GameSceneType.STAGE_2:
                Stage_2Coin();
                break;
            case GameSceneType.STAGE_3:
                Stage_3Coin();
                break;
        }
    }

    void Update()
    {
        // 獲得したコイン数 / ステージに配置したコイン数
        coinNumText.text = $"Coin：{coinScript.GetPlayerCoin}／{stageCoins.Length}";

        ChangeSprite();
    }

    // コイン画像に変更する
    void ChangeSprite()
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

//--------ステージごとの処理--------

    // ステージセレクトでの処理
    void StageSelectCoin()
    {
        onStageSelect = true;
        
        bool onStage_1 = false;
        bool onStage_2 = false;
        bool onStage_3 = false;
    }

    // ステージ1での処理
    void Stage_1Coin()
    {
        onStage_1 = true;

        bool onStageSelect = false;
        bool onStage_2 = false;
        bool onStage_3 = false;
    }

    // ステージ2での処理
    void Stage_2Coin()
    {
        onStage_2 = true;

        bool onStageSelect = false;
        bool onStage_1 = false;
        bool onStage_3 = false;
    }

    // ステージ3での処理
    void Stage_3Coin()
    {
        onStage_3 = true;

        bool onStageSelect = false;
        bool onStage_1 = false;
        bool onStage_2 = false;
    }

//-------------------------------
}
