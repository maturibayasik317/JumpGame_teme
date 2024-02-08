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
    bool onSelect = false;
    public bool GetOnSelect => onSelect;
    [SerializeField] GameObject titleButton; // タイトルボタン

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
        titleButton.SetActive(false);

        Debug.Log(SceneManager.GetActiveScene().name);
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
        onSelect = true;
        SceneManager.LoadScene("Select_Scene");
    }

    // タイトルボタンを押したとき
    public void Title()
    {
        SceneManager.LoadScene("Title_Scene");
    }

    // テキスト、ボタン関連の表示・非表示
    void TextAndButton()
    {
        if (player == null)
        {
            retryButton.SetActive(true);
            selectButton.SetActive(true);
            gameOverImage.SetActive(true);
            titleButton.SetActive(true);
        }
        if (playerScript.GetIsClear)
        {
            retryButton.SetActive(true);
            selectButton.SetActive(true);
            gameClearImage.SetActive(true);
            titleButton.SetActive(true);
        }
    }
}
