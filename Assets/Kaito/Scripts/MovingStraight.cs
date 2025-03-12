using UnityEngine;

/// <summary>
/// 一直線に飛んでくる障害物のクラス
/// </summary>
public class MovingStraight : MonoBehaviour
{
    [SerializeField] float speed; // 移動速度
    [SerializeField] float rotationSpeed; // 回転速度

    SpriteRenderer sr = null;
    bool isView = false;

    void Start()
    {
        sr = GetComponent <SpriteRenderer>();
    }

    void Update()
    {
        if (sr.isVisible) // 画面内なら
        {
            isView = true;
            if (isView)
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            isView = false;
        }
    }
}
