using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----X軸方向で足場を往復させるスクリプト----
public class MovingFloorX : MonoBehaviour
{
    // コンポーネント
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SurfaceEffector2D surfaceEffector;
    [SerializeField] SpriteRenderer sr;

    //[SerializeField] bool xAxis;
    //[SerializeField] bool yAxis;

    [SerializeField] float xRange; // X軸方向の移動範囲
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

    // 動きの処理
    void Move()
    {
        prevPos = transform.position; // 現在位置取得
        float t = Time.time * speed;

        // X座標のみ横移動 : Mathf.PingPongは「t」で移動速度、「xRange」で移動距離
        Vector2 pos = new Vector2(defaultPos.x + Mathf.PingPong(t, xRange), defaultPos.y);
        rb.MovePosition(pos);

        // 速度を逆算する
        Vector2 velocity = (pos - prevPos) / Time.deltaTime * 50;

        // 速度のX成分をSurfaceEffector2Dに適用（上に乗っているオブジェクトのスピードにプラスされる）
        surfaceEffector.speed = velocity.x;
    }
}
