using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----UI系のスクリプト----
public class UI : MonoBehaviour
{
//----------コイン関連----------
    [SerializeField] GameObject[] stageCoins; // ステージ上に配置するコイン
    
    [SerializeField] Sprite coinSprite; // コイン画像
    [SerializeField] Image[] coinImages; // UIとして表示するコイン
    [SerializeField] GameObject coinPrefabs;

    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    Coin coinScript;
//----------コイン関連----------

//----------リトライ・ゲームクリア（オーバー）----------
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject gameClearImage;
    [SerializeField] GameObject gameOverImage;

    [SerializeField] GameObject player;
    Jump_Dash playerScript;
//----------リトライ・ゲームクリア（オーバー）----------

    void Start()
    {
        coinScript = coinPrefabs.GetComponent<Coin>();
        playerScript = player.GetComponent<Jump_Dash>();
        
        gameClearImage.SetActive(false);
        gameOverImage.SetActive(false);
        retryButton.SetActive(false);
    }

    void Update()
    {
        // 獲得したコイン数 / ステージに配置したコイン数
        coinNumText.text = $"Coin：{coinScript.GetPlayerCoin}／{stageCoins.Length}";
        ChangeSprite();
        
        TextAndButton();
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

    public void Retry()
    {
        // 現在開いているシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // テキスト、ボタン関連の表示・非表示
    void TextAndButton()
    {
        if (!player)
        {
            retryButton.SetActive(true);
            gameOverImage.SetActive(true);
        }
        if (playerScript.GetIsClear)
        {
            retryButton.SetActive(true);
            gameClearImage.SetActive(true);
        }
    }
}
