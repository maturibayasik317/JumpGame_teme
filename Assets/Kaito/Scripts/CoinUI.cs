using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----獲得したコインの数を表示するUI----
public class CoinUI : MonoBehaviour
{
    [SerializeField] GameObject[] coinArr; // 配置するコイン
    // 獲得したコイン数を表示するテキスト
    [SerializeField] Text coinNumText;

    PlayerGetCoin playerCoinScript;

    void Start()
    {
        playerCoinScript = GameObject.Find("Player").GetComponent<PlayerGetCoin>();
    }

    void Update()
    {
        coinNumText.text = $"Coin：{playerCoinScript.GetPlayerCoin}／{coinArr.Length}";
    }

    
}
