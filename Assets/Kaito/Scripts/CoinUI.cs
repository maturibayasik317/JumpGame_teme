using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----�l�������R�C���̐���\������UI----
public class CoinUI : MonoBehaviour
{
    [SerializeField] GameObject[] stageCoins; // �X�e�[�W��ɔz�u����R�C��
    
    [SerializeField] Sprite coinSprite; // �R�C���摜
    [SerializeField] Image[] coinImages; // UI�Ƃ��ĕ\������R�C��
    
    // �l�������R�C������\������e�L�X�g
    [SerializeField] Text coinNumText;

    PlayerGetCoin playerCoinScript;

    void Start()
    {
        playerCoinScript = GameObject.Find("Player").GetComponent<PlayerGetCoin>();
    }

    void Update()
    {
        // �l�������R�C���� / �X�e�[�W�ɔz�u�����R�C����
        coinNumText.text = $"Coin�F{playerCoinScript.GetPlayerCoin}�^{stageCoins.Length}";
        ChangeSprite();
    }

    // �R�C���摜�ɕύX����
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
