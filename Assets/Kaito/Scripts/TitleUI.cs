using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//-----タイトル画面UI-----
public class TitleUI : MonoBehaviour
{
    [SerializeField] GameObject stageSelectButton;

    // ボタン押下時の効果音用
    AudioSource audioSource;

    void Start()
    {
        audioSource = stageSelectButton.GetComponent<AudioSource>();
    }

    public void StageSelect()
    {
        StartCoroutine(SceneChange_Select());
    }

    // ステージ選択画面へ
    IEnumerator SceneChange_Select()
    {
        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(0.5f);
        // 音が鳴ってから
        SceneManager.LoadScene("Select_Scene");
    }
}
