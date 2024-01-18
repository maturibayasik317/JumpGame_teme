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
    [SerializeField] float otherSpeed; // 上に乗っているオブジェクトのスピードに関係

    // 計算用
    Vector2 defaultPos;
    Vector2 prevPos;
    float elapsedTime = 0; // 時間計測用

    void Start()
    {
        defaultPos = transform.position; // 初期位置取得
    }

    void FixedUpdate()
    {
        if (sr.isVisible) // 画面内なら
        {
            elapsedTime += Time.deltaTime;

            // x軸方向に移動
            if (xAxis)
            {
                transform.position += new Vector3(speed, 0) * Time.deltaTime;

                if (elapsedTime >= moveTime)
                {
                    // 逆方向に移動
                    transform.position += new Vector3(-speed, 0) * Time.deltaTime;
                    elapsedTime = 0;
                }

                // 速度のX成分をSurfaceEffector2Dに適用（上に乗っているオブジェクトのスピードにプラスされる）
                // surfaceEffector.speed = velocity.x;
            }
            // y軸方向に移動
            else if (yAxis)
            {
                transform.position += new Vector3(0, speed) * Time.deltaTime;

                if (elapsedTime >= moveTime)
                {
                    // 逆方向に移動
                    transform.position += new Vector3(0, -speed) * Time.deltaTime;
                    elapsedTime = 0;
                }

                //// y座標のみ横移動 : Mathf.PingPongは「t」で移動速度、「moveTime」で移動距離
                //Vector2 pos = new Vector2(defaultPos.x, defaultPos.y + Mathf.PingPong(t, moveTime));
                //Vector2 velocity = (pos - prevPos) / Time.deltaTime * otherSpeed;

                //// 速度のy成分をSurfaceEffector2Dに適用（上に乗っているオブジェクトのスピードにプラスされる）
                //surfaceEffector.speed = velocity.y;
            }
        }
        
    }
}
