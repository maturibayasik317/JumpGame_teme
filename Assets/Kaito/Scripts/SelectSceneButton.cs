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
    [SerializeField] GameObject coinManager;
    CoinManager coinManagerScript;

    void Start()
    {
        if (coinPrefab != null) // 参照エラー回避
        {
            coinScript = coinPrefab.GetComponent<Coin>();
        }
        
        coinManager = GameObject.Find("CoinManager");
        coinManagerScript = coinManager.GetComponent<CoinManager>();
    }

    // ボタンを押したときの処理（シーン遷移）
    public void StageButton()
    {
        switch (buttonType)
        {
            case ButtonType.STAGE_1:
                Debug.Log("ステージ1へ");
                SceneManager.LoadScene("TestStage_1");
                // 本実装
                //SceneManager.LoadScene("Ren_Scene1");
                break;
            case ButtonType.STAGE_2:
                SceneManager.LoadScene("TestStage_2");
                // 本実装
                //SceneManager.LoadScene("Stage2_Scene");
                break;
            case ButtonType.STAGE_3:
                SceneManager.LoadScene("TestStage_3");
                // 本実装
                //SceneManager.LoadScene("Stage3_Scene");
                break;
            case ButtonType.STAGE_4:
                SceneManager.LoadScene("TestStage_4"); // 仮
                // ステージ4へ
                break;
            case ButtonType.COINRESET:
                CoinReset();
                break;
            default:
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
        for (int i = 0; i < coinManagerScript.IsPlaerCoin_Stage1.Length; i++)
        {
            coinManagerScript.IsPlaerCoin_Stage1[i] = false;
        }
        for (int i = 0; i < coinManagerScript.IsPlaerCoin_Stage2.Length; i++)
        {
            coinManagerScript.IsPlaerCoin_Stage1[i] = false;
        }
        for (int i = 0; i < coinManagerScript.IsPlaerCoin_Stage3.Length; i++)
        {
            coinManagerScript.IsPlaerCoin_Stage1[i] = false;
        }
        for (int i = 0; i < coinManagerScript.IsPlaerCoin_Stage4.Length; i++)
        {
            coinManagerScript.IsPlaerCoin_Stage1[i] = false;
        }
    }
}
