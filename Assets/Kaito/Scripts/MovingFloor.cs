using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----動く床（修正中）----
public class MovingFloor : MonoBehaviour
{
    [SerializeField] SurfaceEffector2D surfaceEffector;
    [SerializeField] SpriteRenderer sr;

    // 動く軸を設定（インスペクター上でチェックを入れる）
    [SerializeField] bool xAxis;
    [SerializeField] bool yAxis;

    [SerializeField] float moveTime; // 一方向への移動時間
    [SerializeField] float speed; // 移動速度
    [SerializeField] float otherSpeed;

    Vector2 prevPos;
    Vector2 playerPos;
    float elapsedTime = 0; // 時間計測用

    void FixedUpdate()
    {
        if (sr.isVisible) // 画面内なら
        {
            Move();
        }
    }

    // 床を動かす
    void Move()
    {
        elapsedTime += Time.deltaTime; // 経過時間

        // x軸方向に移動
        if (xAxis)
        {
            // 速度
            Vector2 velocity = new Vector2(speed, 0) * Time.deltaTime;
            transform.localPosition += (Vector3)velocity;
            prevPos = transform.position;

            if (elapsedTime >= moveTime)
            {
                // 逆方向に移動
                speed *= -1;
                elapsedTime = 0;
            }
            Vector2 otherVelocity = (velocity - prevPos);
            // 速度のX成分をSurfaceEffector2Dに適用（上に乗っているオブジェクトのスピードにプラスされる）
            surfaceEffector.speed = otherVelocity.x;
        }

        // y軸方向に移動
        if (yAxis)
        {
            // 速度
            Vector2 velocity = new Vector2(0, speed) * Time.deltaTime;
            transform.localPosition += (Vector3)velocity;

            if (elapsedTime >= moveTime)
            {
                // 逆方向に移動
                speed *= -1;
                elapsedTime = 0;
            }
            // y軸で動く際はオブジェクトは横移動させない
            surfaceEffector.speed = 0;
        }
    }
}
