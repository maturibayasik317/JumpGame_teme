using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームシーンでのUIを制御するクラス
/// </summary>
public class MainGameUI : MonoBehaviour
{
    const float WAIT_TIME = 0.5f;

//----------リトライ・ゲームクリア（オーバー）----------
    [SerializeField] GameObject retryButton; // リトライボタン
    [SerializeField] GameObject gameClearImage;
    [SerializeField] GameObject gameOverImage;
    [SerializeField] GameObject selectButton; // ステージ選択ボタン
    [SerializeField] GameObject titleButton; // タイトルボタン

    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem clearParticle;
    [SerializeField] float effectTime;

    Jump_Dash playerScript;
//----------リトライ・ゲームクリア（オーバー）----------

    // ボタン押下時の効果音用
    AudioSource retryAudioSource;
    AudioSource selectAudioSource;
    AudioSource titleAudioSource;

    void Start()
    {
        playerScript = player.GetComponent<Jump_Dash>();

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
    /// <summary>
    /// テキスト、ボタンの表示・非表示を行う関数
    /// </summary>
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

    /// <summary>
    /// クリア演出を行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearEffect()
    {
        if (clearParticle != null) // 参照エラー回避
        {
            clearParticle.Play();
            float stopTime = clearParticle.main.duration + effectTime;
            yield return new WaitForSeconds(stopTime);
            Destroy(clearParticle);
        }
    }

    /// <summary>
    /// リトライを行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneRetry()
    {
        Debug.Log(retryAudioSource);
        retryAudioSource.PlayOneShot(retryAudioSource.clip);
        yield return new WaitForSeconds(WAIT_TIME);
        // 音が鳴ってから
        // 現在のシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ステージ選択画面へ遷移する関数
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneChange_Select()
    {
        selectAudioSource.PlayOneShot(selectAudioSource.clip);
        yield return new WaitForSeconds(WAIT_TIME);
        SceneManager.LoadScene("StageSelect");
    }

    /// <summary>
    /// タイトル画面へ遷移する関数
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneChange_Title()
    {
        titleAudioSource.PlayOneShot(titleAudioSource.clip);
        yield return new WaitForSeconds(WAIT_TIME);
        SceneManager.LoadScene("Title");
    }
}
