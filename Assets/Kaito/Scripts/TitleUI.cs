using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//-----�^�C�g�����UI-----
public class TitleUI : MonoBehaviour
{
    [SerializeField] GameObject stageSelectButton;

    // �X�e�[�W�I����ʂ�
    public void StageSelect()
    {
        SceneManager.LoadScene("Select_Scene");
    }
}
