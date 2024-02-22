using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//-----�^�C�g�����UI-----
public class TitleUI : MonoBehaviour
{
    [SerializeField] GameObject stageSelectButton;

    // �{�^���������̌��ʉ��p
    AudioSource audioSource;

    void Start()
    {
        audioSource = stageSelectButton.GetComponent<AudioSource>();
    }

    public void StageSelect()
    {
        StartCoroutine(SceneChange_Select());
    }

    // �X�e�[�W�I����ʂ�
    IEnumerator SceneChange_Select()
    {
        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(0.5f);
        // �������Ă���
        SceneManager.LoadScene("Select_Scene");
    }
}
