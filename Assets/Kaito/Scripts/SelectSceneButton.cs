using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//----ステージを選択するボタン----
public class SelectSceneButton : MonoBehaviour
{
    // ボタンの種類（どのステージに対応しているか）
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

    // ボタン押下時の効果音用
    AudioSource audioSource;

    void Start()
    {
        if (coinPrefab != null) // 参照エラー回避
        {
            coinScript = coinPrefab.GetComponent<Coin>();
        }

        audioSource = GetComponent<AudioSource>();
        coinManagerScript = GameObject.Find("CoinManager").GetComponent<CoinManager>();
    }

    // ボタンを押したとき
    public void StageButton()
    {
        StartCoroutine(Button());
    }

    // ボタン毎の処理内容（シーン遷移）
    IEnumerator Button()
    {
        switch (buttonType)
        {
            case ButtonType.STAGE_1:
                Debug.Log("ステージ1へ");
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(0.5f);
                // 音が鳴ってから
                SceneManager.LoadScene("Ren_Scene1");
                break;
            case ButtonType.STAGE_2:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(0.5f);
                // 音が鳴ってから
                SceneManager.LoadScene("Player_test_ren");
                break;
            case ButtonType.STAGE_3:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(0.5f);
                // 音が鳴ってから
                SceneManager.LoadScene("Stage2_Scene");
                break;
            case ButtonType.STAGE_4:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(0.5f);
                // 音が鳴ってから
                SceneManager.LoadScene("Stage3_Scene");
                break;
            case ButtonType.COINRESET:
                audioSource.PlayOneShot(audioSource.clip);
                CoinReset();
                break;
        }
    }

    // 全ステージのコイン枚数をリセット
    void CoinReset()
    {
        coinScript.PlayerCoin_Stage1 = 0;
        coinScript.PlayerCoin_Stage2 = 0;
        coinScript.PlayerCoin_Stage3 = 0;
        coinScript.PlayerCoin_Stage4 = 0;

        // 取得状態をリセット
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
