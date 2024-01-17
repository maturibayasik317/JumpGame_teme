using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----�l�������R�C���̐���\������UI----
public class CoinUI : MonoBehaviour
{
    [SerializeField] GameObject[] coinArr; // �z�u����R�C��
    // �l�������R�C������\������e�L�X�g
    [SerializeField] Text coinNumText;

    PlayerGetCoin playerCoinScript;

    void Start()
    {
        playerCoinScript = GameObject.Find("Player").GetComponent<PlayerGetCoin>();
    }

    void Update()
    {
        coinNumText.text = $"Coin�F{playerCoinScript.GetPlayerCoin}�^{coinArr.Length}";
    }

    
}
