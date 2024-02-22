using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----ゲームシーンでのUI系のスクリプト----
public class MainGameUI : MonoBehaviour
{
//----------リトライ・ゲームクリア（オーバー）等----------
    [SerializeField] GameObject retryButton; // リトライボタン
    [SerializeField] GameObject gameClearImage;
    [SerializeField] GameObject gameOverImage;
    [SerializeField] GameObject selectButton; // ステージ選択ボタン
    [SerializeField] GameObject titleButton; // タイトルボタン

    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem clearParticle;
    [SerializeField] float effectTime;

    Jump_Dash playerScript; // 本実装
    //PlayerTest playerScript; // テスト
//----------リトライ・ゲームクリア（オーバー）----------

    // ボタン押下時の効果音用
    AudioSource retryAudioSource;
    AudioSource selectAudioSource;
    AudioSource titleAudioSource;

    void Start()
    {
        // 本実装
        playerScript = player.GetComponent<Jump_Dash>();
        //playerScript = player.GetComponent<PlayerTest>();

        retryAudioSource = retryButton.GetComponent<AudioSource>();
        selectAudioSource = selectButton.GetComponent<AudioSource>();
        titleAudioSource = titleButton.GetComponent<AudioSource>();

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
        StartCoroutine(SceneRetry());
    }

    // セレクトボタンを押したとき
    public void Select()
    {
        StartCoroutine(SceneChange_Select());
    }

    // タイトルボタンを押したとき
    public void Title()
    {
        StartCoroutine(SceneChange_Title());
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

            StartCoroutine(ClearEffect());
        }
    }

    // クリア演出
    IEnumerator ClearEffect()
    {
        if (clearParticle != null) // 参照エラー回避
        {
            clearParticle.Play();
            float stopTime = clearParticle.main.duration + effectTime;
            yield return new WaitForSeconds(stopTime);

            //「duration」 + 「effectTime」秒後にストップさせる
            Destroy(clearParticle);
        }
    }


    // リトライ
    IEnumerator SceneRetry()
    {
        Debug.Log(retryAudioSource);
        retryAudioSource.PlayOneShot(retryAudioSource.clip);
        yield return new WaitForSeconds(0.5f);
        // 音が鳴ってから
        // 現在開いているシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // ステージ選択画面へ
    IEnumerator SceneChange_Select()
    {
        selectAudioSource.PlayOneShot(selectAudioSource.clip);
        yield return new WaitForSeconds(0.5f);
        // 音が鳴ってから
        SceneManager.LoadScene("Select_Scene");
    }
    // タイトル画面へ
    IEnumerator SceneChange_Title()
    {
        titleAudioSource.PlayOneShot(titleAudioSource.clip);
        yield return new WaitForSeconds(0.5f);
        // 音が鳴ってから
        SceneManager.LoadScene("Title_Scene");
    }
}
