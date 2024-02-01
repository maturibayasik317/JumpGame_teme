using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----ゲームシーンでのUI系のスクリプト----
public class MainGameUI : MonoBehaviour
{
//----------リトライ・ゲームクリア（オーバー）等----------
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject gameClearImage;
    [SerializeField] GameObject gameOverImage;
    [SerializeField] GameObject selectButton; // ステージ選択ボタン

    [SerializeField] GameObject player;
    //Jump_Dash playerScript; // 本実装
    PlayerTest playerScript;
//----------リトライ・ゲームクリア（オーバー）----------

    void Start()
    {
        // 本実装
        //playerScript = player.GetComponent<Jump_Dash>();
        playerScript = player.GetComponent<PlayerTest>();
        
        gameClearImage.SetActive(false);
        gameOverImage.SetActive(false);
        retryButton.SetActive(false);
        selectButton.SetActive(false);
    }

    void Update()
    {
        TextAndButton();
    }

    // リトライボタンを押したとき
    public void Retry()
    {
        // 現在開いているシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // セレクトボタンを押したとき
    public void Select()
    {
        SceneManager.LoadScene("Select_Scene");
    }

    // テキスト、ボタン関連の表示・非表示
    void TextAndButton()
    {
        if (!player)
        {
            retryButton.SetActive(true);
            selectButton.SetActive(true);
            gameOverImage.SetActive(true);
        }
        if (playerScript.GetIsClear)
        {
            retryButton.SetActive(true);
            selectButton.SetActive(true);
            gameClearImage.SetActive(true);
        }
    }
}
