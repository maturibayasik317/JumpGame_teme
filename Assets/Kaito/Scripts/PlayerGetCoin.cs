using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----プレイヤーが獲得したコインを記録する----
// プレイヤーにアタッチする
public class PlayerGetCoin : MonoBehaviour
{
    int coinNum = 0; // プレイヤーが獲得したコイン

    public int GetPlayerCoin => coinNum;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinNum += 1;
        }
    }
}
