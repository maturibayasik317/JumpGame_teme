using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//-----タイトル画面UI-----
public class TitleUI : MonoBehaviour
{
    [SerializeField] GameObject stageSelectButton;

    // ステージ選択画面へ
    public void StageSelect()
    {
        SceneManager.LoadScene("Select_Scene");
    }
}
