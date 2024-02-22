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

    // �{�^���������̌��ʉ��p
    AudioSource audioSource;

    void Start()
    {
        if (coinPrefab != null) // �Q�ƃG���[���
        {
            coinScript = coinPrefab.GetComponent<Coin>();
        }

        audioSource = GetComponent<AudioSource>();
        coinManagerScript = GameObject.Find("CoinManager").GetComponent<CoinManager>();
    }

    // �{�^�����������Ƃ�
    public void StageButton()
    {
        StartCoroutine(Button());
    }

    // �{�^�����̏������e�i�V�[���J�ځj
    IEnumerator Button()
    {
        switch (buttonType)
        {
            case ButtonType.STAGE_1:
                Debug.Log("�X�e�[�W1��");
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(0.5f);
                // �������Ă���
                SceneManager.LoadScene("Ren_Scene1");
                break;
            case ButtonType.STAGE_2:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(0.5f);
                // �������Ă���
                SceneManager.LoadScene("Player_test_ren");
                break;
            case ButtonType.STAGE_3:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(0.5f);
                // �������Ă���
                SceneManager.LoadScene("Stage2_Scene");
                break;
            case ButtonType.STAGE_4:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(0.5f);
                // �������Ă���
                SceneManager.LoadScene("Stage3_Scene");
                break;
            case ButtonType.COINRESET:
                audioSource.PlayOneShot(audioSource.clip);
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
