using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�v���C���[���l�������R�C�����L�^����----
// �v���C���[�ɃA�^�b�`����
public class PlayerGetCoin : MonoBehaviour
{
    int coinNum = 0; // �v���C���[���l�������R�C��

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
