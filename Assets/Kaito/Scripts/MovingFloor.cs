using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----動く床（修正中）----
public class MovingFloor : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    // 動く軸を設定（インスペクター上でチェックを入れる）
    [SerializeField] bool xAxis;
    [SerializeField] bool yAxis;

    [SerializeField] float moveTime; // 一方向への移動時間
    [SerializeField] float speed; // 移動速度

    Vector2 oldPos = Vector2.zero; // 直前の位置を取得
    Vector2 velocity = Vector2.zero;
    Vector2 playerVelocity = Vector2.zero; // プレイヤーに加算する速度
    float elapsedTime = 0; // 時間計測用

    // 読み取り専用プロパティ
    public Vector2 GetVelocity => playerVelocity;

    void Start()
    {
        oldPos = transform.position;
    }

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
            velocity.x = speed * Time.deltaTime;
            transform.localPosition += (Vector3)velocity;

            if (elapsedTime >= moveTime)
            {
                // 逆方向に移動
                speed *= -1;
                elapsedTime = 0;
            }

            // 進んだ距離を出し、時間で割る
            playerVelocity.x = (transform.localPosition.x - oldPos.x) / Time.deltaTime;
            oldPos.x = transform.localPosition.x; // 直前の床の位置を保存
        }

        // y軸方向に移動
        if (yAxis)
        {
            // 速度
            velocity.y = speed * Time.deltaTime;
            transform.localPosition += (Vector3)velocity;
            
            if (elapsedTime >= moveTime)
            {
                // 逆方向に移動
                speed *= -1;
                elapsedTime = 0;
            }

            playerVelocity.y = (transform.localPosition.y - oldPos.y) / Time.deltaTime;
            oldPos.y = transform.localPosition.y;
        }
    }
}
