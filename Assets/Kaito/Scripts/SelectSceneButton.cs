using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージを選択するボタンクラス
/// </summary>
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

    /// <summary>
    /// 各ステージのボタンを押したら呼ばれる関数
    /// </summary>
    public void StageButton()
    {
        StartCoroutine(Button());
    }

    /// <summary>
    /// 押したボタンごとに別のシーンに遷移する関数
    /// </summary>
    /// <returns></returns>
    IEnumerator Button()
    {
        const float WAIT_TIME = 0.5f;

        switch (buttonType)
        {
            case ButtonType.STAGE_1:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(WAIT_TIME);
                // 音が鳴ってから
                SceneManager.LoadScene("Stage_1");
                break;
            case ButtonType.STAGE_2:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(WAIT_TIME);
                SceneManager.LoadScene("Stage_2");
                break;
            case ButtonType.STAGE_3:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(WAIT_TIME);
                SceneManager.LoadScene("Stage_3");
                break;
            case ButtonType.STAGE_4:
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(WAIT_TIME);
                SceneManager.LoadScene("Stage_4");
                break;
            case ButtonType.COINRESET:
                audioSource.PlayOneShot(audioSource.clip);
                CoinReset();
                break;
        }
    }

    /// <summary>
    /// 全ステージのコイン枚数をリセット関数
    /// </summary>
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
