using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----Y軸方向に往復する敵----
public class Enemy : MonoBehaviour
{
    [SerializeField] float yRange; // 移動範囲
    [SerializeField] float speed; // 移動速度

    // 空のオブジェクトで管理しているため、オブジェクトの中心位置が映らないといけない
    // 最初にnullにしておかないと、画面外でも勝手に動いてしまう
    SpriteRenderer sr = null;
    bool isView = false;
    Vector2 startPos;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (sr.isVisible) // 敵がカメラの範囲内なら
        {
            isView = true; // カメラ内判定に
            if (isView)
            {
                float t = Time.time * speed;

                // Y座標のみ横移動
                float pos = startPos.y + Mathf.PingPong(t, yRange);
                transform.position = new Vector3(startPos.x, pos, transform.position.z);
            }
        }
        else
        {
            isView = false;
        }
    }
}
