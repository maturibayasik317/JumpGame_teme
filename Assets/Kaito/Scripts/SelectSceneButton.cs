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
        COINRESET
    }
    [SerializeField] ButtonType buttonType;

    [SerializeField] GameObject coinPrefab;
    Coin coinScript;

    void Start()
    {
        if (coinPrefab != null) // 参照エラー回避
        {
            coinScript = coinPrefab.GetComponent<Coin>();
        }
    }

    // ボタンを押したときの処理（シーン遷移）
    public void StageButton()
    {
        switch (buttonType)
        {
            case ButtonType.STAGE_1:
                Debug.Log("ステージ1へ");
                //SceneManager.LoadScene("TestStage_1");
                SceneManager.LoadScene("Ren_Scene1");
                break;
            case ButtonType.STAGE_2:
                //SceneManager.LoadScene("TestStage_2");
                SceneManager.LoadScene("Stage2_Scene");
                break;
            case ButtonType.STAGE_3:
                //SceneManager.LoadScene("TestStage_3");
                SceneManager.LoadScene("Stage3_Scene");
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

        coinScript.GetIsPlayer = false;
    }

    //// フェードイン・フェードアウト用
    //[SerializeField] GameObject fadePanel;
    //[SerializeField] float fadeWaitTime;
    //GameObject fadeCanvasClone;
    //FadeCanvas fadeScript;
    ////

    //IEnumerator Fade()
    //{

    //}
}
