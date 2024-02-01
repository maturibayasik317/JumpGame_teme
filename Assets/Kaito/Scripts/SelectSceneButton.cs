using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//----ステージを選択するボタン----
public class SelectSceneButton : MonoBehaviour
{
    [SerializeField] ButtonType buttonType;

    //// フェードイン・フェードアウト用
    //[SerializeField] GameObject fadePanel;
    //[SerializeField] float fadeWaitTime;
    //GameObject fadeCanvasClone;
    //FadeCanvas fadeScript;
    ////

    // ボタンの種類（どのステージに対応しているか）
    enum ButtonType
    {
        STAGE_1,
        STAGE_2,
        STAGE_3
    }

    // ボタンを押したときの処理（シーン遷移）
    public void StageButton()
    {
        switch (buttonType)
        {
            case ButtonType.STAGE_1:
                Debug.Log("ステージ1へ");
                SceneManager.LoadScene("Ren_Scene1");
                break;
            case ButtonType.STAGE_2:
                SceneManager.LoadScene("Stage2_Scene");
                break;
            case ButtonType.STAGE_3:
                SceneManager.LoadScene("Stage3_Scene");
                break;
            default:
                break;
        }
    }

    //IEnumerator Fade()
    //{

    //}
}
