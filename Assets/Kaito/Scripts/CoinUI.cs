using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----獲得したコインの数を表示するUI----
public class CoinUI : MonoBehaviour
{
    [SerializeField] GameObject[] stageCoins; // ステージ上に配置するコイン
    
    [SerializeField] Sprite coinSprite; // コイン画像
    [SerializeField] Image[] coinImages; // UIとして表示するコイン
    
    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    PlayerGetCoin playerCoinScript;

    void Start()
    {
        playerCoinScript = GameObject.Find("Player").GetComponent<PlayerGetCoin>();
    }

    void Update()
    {
        // 獲得したコイン数 / ステージに配置したコイン数
        coinNumText.text = $"Coin：{playerCoinScript.GetPlayerCoin}／{stageCoins.Length}";
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
}
