using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージ選択画面のUIを管理するクラス
/// </summary>
public class StageSelectUI : MonoBehaviour
{
    const int IMAGE_NUM = 12;

    [SerializeField] Sprite coinSprite;
    [SerializeField] Image[] coinImages = new Image[IMAGE_NUM];

    // ボタンの種類（どのステージに対応しているか）
    enum ButtonType
    {
        STAGE_1,
        STAGE_2,
        STAGE_3,
        STAGE_4,
        COINRESET
    }

    [SerializeField] GameObject coinPrefab;
    Coin coinScript;
    CoinManager coinManagerScript;

    // ボタン押下時の効果音用
    AudioSource audioSource;

    void Awake()
    {
        if (coinPrefab != null)
        {
            coinScript = coinPrefab.GetComponent<Coin>();
        }

        audioSource = GetComponent<AudioSource>();
        coinManagerScript = GameObject.Find("CoinManager").GetComponent<CoinManager>();
        
        for (int num = 0; num < IMAGE_NUM; num++)
        {
            Image image = GameObject.Find("CoinImage_" + num).GetComponent<Image>();
            coinImages[num] = image;
        }
    }

    void Start()
    {
        #if false // 変更前
        // ステージ1
        for (int i = 0; i < coinManagerScript.IsPlayerCoin_Stage1.Length; i++)
        {
            if (coinManagerScript.IsPlayerCoin_Stage1[i]) coinImages[i].sprite = coinSprite;
        }
        // ステージ2
        for (int i = 0; i < coinManagerScript.IsPlayerCoin_Stage2.Length; i++)
        {
            if (coinManagerScript.IsPlayerCoin_Stage2[i]) coinImages[i + 3].sprite = coinSprite;
        }
        // ステージ3
        for (int i = 0; i < coinManagerScript.IsPlayerCoin_Stage3.Length; i++)
        {
            if (coinManagerScript.IsPlayerCoin_Stage3[i]) coinImages[i + 6].sprite = coinSprite;
        }
        // ステージ4
        for (int i = 0; i < coinManagerScript.IsPlayerCoin_Stage4.Length; i++)
        {
            if (coinManagerScript.IsPlayerCoin_Stage4[i]) coinImages[i + 9].sprite = coinSprite;
        }
        #endif

        int imageIdx = 0;
        // 各ステージのコイン取得状況を調べ、取得済みなら画像をコインに変更
        for (int sNum = 0; sNum < GlobalConst.MAX_STAGE_NUM; sNum++)
        {
            for (int cNum = 0; cNum < GlobalConst.MAX_COIN_NUM; cNum++)
            {
                imageIdx++;
                if (!coinManagerScript.GetPlayerHaveCoins(sNum, cNum)) return;
                coinImages[imageIdx].sprite = coinSprite;
            }
        }
    }

    /// <summary>
    /// 押したボタンごとにシーン遷移やコインをリセットする関数
    /// </summary>
    /// <returns></returns>
    IEnumerator Button(ButtonType _buttonType)
    {
        const float WAIT_TIME = 0.5f;

        switch (_buttonType)
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
    /// 全ステージのコイン枚数をリセットする関数
    /// </summary>
    void CoinReset()
    {
        // 取得状態をリセット
        coinScript.ResetPlayerCoinNums();
        coinManagerScript.ResetPlayerHaveCoins(GlobalConst.MAX_STAGE_NUM);
    }

    /* 各ボタンを押したら呼ばれる関数*/
    public void CoinRese_Button() // 全ステージのコイン取得状況をリセット
    {
        StartCoroutine(Button(ButtonType.COINRESET));
    }
    public void Stage1_Button() // ステージ1へ
    {
        StartCoroutine(Button(ButtonType.STAGE_1));
    }
    public void Stage2_Button() // ステージ2へ
    {
        StartCoroutine(Button(ButtonType.STAGE_2));
    }
    public void Stage3_Button() // ステージ3へ
    {
        StartCoroutine(Button(ButtonType.STAGE_3));
    }
    public void Stage4_Button() // ステージ4へ
    {
        StartCoroutine(Button(ButtonType.STAGE_4));
    }

#if false // 変更前
    /// <summary>
    /// 全ステージのコイン枚数をリセットする関数
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
#endif

}
