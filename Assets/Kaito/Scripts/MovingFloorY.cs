using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----Y軸方向で足場を往復させるスクリプト----
public class MovingFloorY : MonoBehaviour
{
    // コンポーネント
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SurfaceEffector2D surfaceEffector;
    [SerializeField] SpriteRenderer sr;

    //[SerializeField] bool xAxis;
    //[SerializeField] bool yAxis;
    [SerializeField] float yRange; // Y軸方向の移動範囲
    [SerializeField] float speed; // 移動速度

    // 計算用
    Vector2 defaultPos;
    Vector2 prevPos;

    void Start()
    {
        defaultPos = transform.position; // 初期位置取得
    }

    void FixedUpdate()
    {
        if (sr.isVisible)
        {
            Move();
        }
    }

    void Move()
    {
        prevPos = rb.position; // 現在位置取得
        float t = Time.time * speed;

        // Y座標のみ横移動 : Mathf.PingPongは「t」で移動速度、「yRange」で移動距離
        Vector2 pos = new Vector2(defaultPos.x, defaultPos.y + Mathf.PingPong(t, yRange));
        rb.MovePosition(pos);

        //// 速度を逆算する
        //Vector2 velocity = (pos - prevPos) / Time.deltaTime * 50;

        //// 速度のY成分をSurfaceEffector2Dに適用（上に乗っているオブジェクトのスピードにプラスされる）
        //surfaceEffector.speed = velocity.y;
    }
}
