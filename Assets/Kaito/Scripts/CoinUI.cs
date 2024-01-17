using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----�l�������R�C���̐���\������UI----
public class CoinUI : MonoBehaviour
{
    [SerializeField] GameObject[] stageCoin; // �X�e�[�W��ɔz�u����R�C��
    [SerializeField] GameObject[] uiCoin; // UI�Ƃ��ĕ\������R�C��
    [SerializeField] Sprite coinSprite; // �R�C���擾��̉摜
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
        coinNumText.text = $"Coin�F{playerCoinScript.GetPlayerCoin}�^{stageCoin.Length}";

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}
