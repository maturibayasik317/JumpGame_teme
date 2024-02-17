using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//----�X�e�[�W��I������{�^��----
public class SelectSceneButton : MonoBehaviour
{
    // �{�^���̎�ށi�ǂ̃X�e�[�W�ɑΉ����Ă��邩�j
    enum ButtonType
    {
        STAGE_1,
        STAGE_2,
        STAGE_3,
        STAGE_4,
        COINRESET
    }
    [SerializeField] ButtonType buttonType;

    [SerializeField] GameObject coinPrefab;
    Coin coinScript;
    
    CoinManager coinManagerScript;

    void Start()
    {
        if (coinPrefab != null) // �Q�ƃG���[���
        {
            coinScript = coinPrefab.GetComponent<Coin>();
        }
        
        coinManagerScript = GameObject.Find("CoinManager").GetComponent<CoinManager>();
    }

    // �{�^�����������Ƃ��̏����i�V�[���J�ځj
    public void StageButton()
    {
        switch (buttonType)
        {
            case ButtonType.STAGE_1:
                Debug.Log("�X�e�[�W1��");
                //SceneManager.LoadScene("TestStage_1");
                // �{����
                SceneManager.LoadScene("Ren_Scene1");
                break;
            case ButtonType.STAGE_2:
                //SceneManager.LoadScene("TestStage_2");
                // �{����
                SceneManager.LoadScene("Stage2_Scene");
                break;
            case ButtonType.STAGE_3:
                //SceneManager.LoadScene("TestStage_3");
                // �{����
                SceneManager.LoadScene("Stage3_Scene");
                break;
            case ButtonType.STAGE_4:
                SceneManager.LoadScene("TestStage_4"); // ��
                // �X�e�[�W4��
                break;
            case ButtonType.COINRESET:
                CoinReset();
                break;
        }
    }

    // �S�X�e�[�W�̃R�C�����������Z�b�g
    void CoinReset()
    {
        coinScript.PlayerCoin_Stage1 = 0;
        coinScript.PlayerCoin_Stage2 = 0;
        coinScript.PlayerCoin_Stage3 = 0;
        coinScript.PlayerCoin_Stage4 = 0;

        // �擾��Ԃ����Z�b�g
        for (int i = 0; i < coinManagerScript.IsPlayerCoin_Stage1.Length; i++)
        {
            coinManagerScript.IsPlayerCoin_Stage1[i] = false;
        }
        for (int i = 0; i < coinManagerScript.IsPlayerCoin_Stage2.Length; i++)
        {
            coinManagerScript.IsPlayerCoin_Stage2[i] = false;
        }
        for (int i = 0; i < coinManagerScript.IsPlayerCoin_Stage3.Length; i++)
        {
            coinManagerScript.IsPlayerCoin_Stage3[i] = false;
        }
        for (int i = 0; i < coinManagerScript.IsPlayerCoin_Stage4.Length; i++)
        {
            coinManagerScript.IsPlayerCoin_Stage4[i] = false;
        }
    }
}
