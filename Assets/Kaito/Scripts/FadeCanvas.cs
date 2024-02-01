using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----フェードイン・フェードアウト処理----
public class FadeCanvas : MonoBehaviour
{
    [SerializeField] Image panelImage;
    [SerializeField] float fadeSpeed;

    bool fadeIn = false;
    bool fadeOut = false;

    float red, green, blue, alpha;

    // プロパティ
    public bool PropertyFadeIn
    {
        set { fadeIn = value;} // 値の代入
        get { return fadeIn;}
    }
    public bool PropertyFadeOut
    {
        set { fadeOut = value;}
        get { return fadeOut;}
    }

    void Start()
    {
        // シーンが変わっても削除されない
        DontDestroyOnLoad(gameObject);

        // 元の色を取得
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

    // フェードイン
    void FadeIn()
    {
        alpha += fadeSpeed;
        SetAlpha();

        if (alpha >= 1)
        {
            fadeIn = false;
        }
    }

    // フェードアウト
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

    // 透明度を変更
    void SetAlpha()
    {
        panelImage.color = new Color(red, green, blue, alpha);
    }
}
