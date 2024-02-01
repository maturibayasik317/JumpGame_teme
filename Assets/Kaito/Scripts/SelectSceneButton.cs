using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//----�X�e�[�W��I������{�^��----
public class SelectSceneButton : MonoBehaviour
{
    [SerializeField] ButtonType buttonType;

    //// �t�F�[�h�C���E�t�F�[�h�A�E�g�p
    //[SerializeField] GameObject fadePanel;
    //[SerializeField] float fadeWaitTime;
    //GameObject fadeCanvasClone;
    //FadeCanvas fadeScript;
    ////

    // �{�^���̎�ށi�ǂ̃X�e�[�W�ɑΉ����Ă��邩�j
    enum ButtonType
    {
        STAGE_1,
        STAGE_2,
        STAGE_3
    }

    // �{�^�����������Ƃ��̏����i�V�[���J�ځj
    public void StageButton()
    {
        switch (buttonType)
        {
            case ButtonType.STAGE_1:
                Debug.Log("�X�e�[�W1��");
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
