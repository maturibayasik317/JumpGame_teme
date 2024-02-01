using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----�t�F�[�h�C���E�t�F�[�h�A�E�g����----
public class FadeCanvas : MonoBehaviour
{
    [SerializeField] Image panelImage;
    [SerializeField] float fadeSpeed;

    bool fadeIn = false;
    bool fadeOut = false;

    float red, green, blue, alpha;

    // �v���p�e�B
    public bool PropertyFadeIn
    {
        set { fadeIn = value;} // �l�̑��
        get { return fadeIn;}
    }
    public bool PropertyFadeOut
    {
        set { fadeOut = value;}
        get { return fadeOut;}
    }

    void Start()
    {
        // �V�[�����ς���Ă��폜����Ȃ�
        DontDestroyOnLoad(gameObject);

        // ���̐F���擾
        red = panelImage.color.r;
        green = panelImage.color.g;
        blue = panelImage.color.b;
        alpha = panelImage.color.a;
    }

    void Update()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        else if (fadeOut)
        {
            FadeOut();
        }
    }

    // �t�F�[�h�C��
    void FadeIn()
    {
        alpha += fadeSpeed;
        SetAlpha();

        if (alpha >= 1)
        {
            fadeIn = false;
        }
    }

    // �t�F�[�h�A�E�g
    void FadeOut()
    {
        alpha -= fadeSpeed;
        SetAlpha();

        if (alpha <= 0)
        {
            fadeOut = false;
            Destroy(gameObject);
        }
    }

    // �����x��ύX
    void SetAlpha()
    {
        panelImage.color = new Color(red, green, blue, alpha);
    }
}
