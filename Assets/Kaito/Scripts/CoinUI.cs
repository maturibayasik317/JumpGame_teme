using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----獲得したコインの数を表示するUI----
public class CoinUI : MonoBehaviour
{
    [SerializeField] GameObject[] stageCoin; // ステージ上に配置するコイン
    [SerializeField] GameObject[] uiCoin; // UIとして表示するコイン
    [SerializeField] Sprite coinSprite; // コイン取得後の画像
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
        coinNumText.text = $"Coin：{playerCoinScript.GetPlayerCoin}／{stageCoin.Length}";

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}
