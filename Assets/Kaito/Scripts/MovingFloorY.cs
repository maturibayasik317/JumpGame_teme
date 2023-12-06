using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----Y軸方向で足場を往復させるスクリプト----
public class MovingFloorY : MonoBehaviour
{
    // コンポーネント
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SurfaceEffector2D surfaceEffector;

    [SerializeField] float yRange; // Y軸方向の移動範囲
    [SerializeField] float speed; // 移動速度？

    // 計算用
    Vector2 defaultPos;
    Vector2 prevPos;

    void Start()
    {
        defaultPos = transform.position;
    }

    void FixedUpdate()
    {
        prevPos = rb.position;

        // Y座標のみ横移動（Mathf.PingPongの数値部分（yRange）変更で移動距離が変わる）
        Vector2 pos = new Vector2(defaultPos.x, defaultPos.y + Mathf.PingPong(Time.time, yRange));
        rb.MovePosition(pos);

        // 速度を逆算する
        Vector2 velocity = (pos - prevPos) / Time.deltaTime * speed;

        // 速度のY成分をSurfaceEffector2Dに適用
        surfaceEffector.speed = velocity.y;
    }
}
